using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Contracts.Tasks;
using TaskFlow.Api.Data;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Domain.Enums;
using TaskFlow.Api.Errors.Exceptions;

namespace TaskFlow.Api.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private const int SqliteUniqueConstraintErrorCode = 2067;
        private const int TaskTitleMaxLength = 200;

        private readonly TaskFlowDbContext _dbContext;

        public TaskService(TaskFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken)
        {

            var trimmedTitle = ValidateAndTrimTitle(request.Title);
            var normalizedTitle = Normalize(trimmedTitle);

            var priority = request.Priority
                ?? throw new ValidationException(
                    "O campo priority é obrigatório.",
                    new Dictionary<string, string[]>
                    {
                        ["priority"] = new[]
                        {
                            "O campo priority é obrigatório."
                        }
                    });

            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(
                    project => project.Id == projectId,
                    cancellationToken);
            if (project is null)
            {
                throw new NotFoundException($"Projeto com id '{projectId}' não foi encontrado.", "project_not_found");
            }

            if (project.Status == ProjectStatus.Archived)
            {
                throw new BusinessRuleViolationException(
                    "Não é possível adicionar tarefas a um projeto arquivado.",
                    "archived_project_does_not_accept_tasks");
            }

            await EnsureUniqueTitleAsync(projectId, normalizedTitle, cancellationToken);

            var task = new TaskItem
            {
                Title = trimmedTitle,
                NormalizedTitle = normalizedTitle,
                Description = request.Description,
                Priority = priority,
                Status = TaskItemStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ProjectId = projectId
            };

            _dbContext.TaskItems.Add(task);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
                when (IsTaskTitleUniqueConstraintViolation(ex))
            {
                throw new ConflictException(
                    "Já existe uma tarefa com este título neste projeto.",
                    "task_title_conflict");
            }

            return TaskResponse.FromEntity(task);
        }

        public async Task<IReadOnlyList<TaskResponse>> ListAsync(
            Guid projectId,
            TaskItemStatus? status,
            TaskPriority? priority,
            CancellationToken cancellationToken)
        {
            var projectExists = await _dbContext.Projects
                .AsNoTracking()
                .AnyAsync(
                    project => project.Id == projectId,
                    cancellationToken);

            if (!projectExists)
            {
                throw new NotFoundException(
                    $"Projeto com id '{projectId}' não foi encontrado.",
                    "project_not_found");
            }

            var query = _dbContext.TaskItems
                .AsNoTracking()
                .Where(task => task.ProjectId == projectId);

            if (status.HasValue)
            {
                query = query.Where(task => task.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(task => task.Priority == priority.Value);
            }

            var tasks = await query.ToListAsync(cancellationToken);

            return tasks
                .Select(TaskResponse.FromEntity)
                .ToList();
        }

        public async Task<TaskResponse> PatchAsync(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            if (!request.Title.HasValue && !request.Description.HasValue && !request.Status.HasValue && !request.Priority.HasValue)
            {
                throw new ValidationException(
                    "O corpo da requisição PATCH deve conter ao menos um campo para atualização.",
                    new Dictionary<string, string[]>
                    {
                        ["requestBody"] = new[] { "O corpo da requisição PATCH deve conter ao menos um campo para atualização." }
                    });
            }

            var task = await _dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (task is null)
            {
                throw new NotFoundException($"Tarefa com id '{id}' não foi encontrada.", "task_not_found");
            }

            if (task.Status == TaskItemStatus.Done)
            {
                var isIsolatedDoneStatus =
                    request.Status.HasValue &&
                    request.Status.Value == TaskItemStatus.Done &&
                    !request.Title.HasValue &&
                    !request.Description.HasValue &&
                    !request.Priority.HasValue;

                if (!isIsolatedDoneStatus)
                {
                    throw new BusinessRuleViolationException(
                        "Tarefa concluída não pode ser modificada.",
                        "completed_task_cannot_be_modified");
                }

                return TaskResponse.FromEntity(task);
            }

            if (request.Title.HasValue)
            {
                var newTitle = ValidateAndTrimTitle(request.Title.Value);
                var normalizedTitle = Normalize(newTitle);

                if (!string.Equals(task.NormalizedTitle, normalizedTitle, StringComparison.Ordinal))
                {
                    await EnsureUniqueTitleAsync(task.ProjectId, normalizedTitle, cancellationToken, task.Id);
                }

                task.Title = newTitle;
                task.NormalizedTitle = normalizedTitle;
            }

            if (request.Description.HasValue)
            {
                task.Description = request.Description.Value;
            }

            if (request.Priority.HasValue)
            {
                task.Priority = request.Priority.Value;
            }

            if (request.Status.HasValue)
            {
                var newStatus = request.Status.Value;
                if (task.Status != newStatus)
                {
                    if (!IsValidStatusTransition(task.Status, newStatus))
                    {
                        throw new BusinessRuleViolationException(
                            "Transição de status inválida.",
                            "invalid_task_status_transition");
                    }

                    // when moving to Done set CompletedAt
                    if (newStatus == TaskItemStatus.Done)
                    {
                        task.CompletedAt = DateTime.UtcNow;
                    }

                    task.Status = newStatus;
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
                when (IsTaskTitleUniqueConstraintViolation(ex))
            {
                throw new ConflictException(
                    "Já existe uma tarefa com este título neste projeto.",
                    "task_title_conflict");
            }

            return TaskResponse.FromEntity(task);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (task is null)
            {
                throw new NotFoundException($"Tarefa com id '{id}' não foi encontrada.", "task_not_found");
            }

            if (task.Status != TaskItemStatus.Pending)
            {
                throw new BusinessRuleViolationException(
                    "Somente tarefas com status pending podem ser excluídas.",
                    "task_cannot_be_deleted");
            }

            _dbContext.TaskItems.Remove(task);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private static string ValidateAndTrimTitle(string? title)
        {
            var trimmed = title?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                throw new ValidationException(
                    "O campo title deve conter pelo menos um caractere diferente de espaço.",
                    new Dictionary<string, string[]>
                    {
                        ["title"] = new[] { "O campo title deve conter ao menos um caractere diferente de espaço." }
                    });
            }

            if (trimmed.Length > TaskTitleMaxLength)
            {
                throw new ValidationException(
                    $"O campo title deve possuir no máximo {TaskTitleMaxLength} caracteres.",
                    new Dictionary<string, string[]>
                    {
                        ["title"] = new[] { $"O campo title deve possuir no máximo {TaskTitleMaxLength} caracteres." }
                    });
            }

            return trimmed;
        }

        private static string Normalize(string value) => value.Trim().ToLowerInvariant();

        private async Task EnsureUniqueTitleAsync(Guid projectId, string normalizedTitle, CancellationToken cancellationToken, Guid? currentTaskId = null)
        {
            var query = _dbContext.TaskItems.AsNoTracking().Where(t => t.ProjectId == projectId && t.NormalizedTitle == normalizedTitle);
            if (currentTaskId.HasValue)
            {
                query = query.Where(t => t.Id != currentTaskId.Value);
            }

            var exists = await query.AnyAsync(cancellationToken);
            if (exists)
            {
                throw new ConflictException(
                    "Já existe uma tarefa com este título neste projeto.",
                    "task_title_conflict");
            }
        }

        private static bool IsValidStatusTransition(TaskItemStatus current, TaskItemStatus next)
        {
            if (current == next) return true; // idempotent

            return current switch
            {
                TaskItemStatus.Pending when next == TaskItemStatus.InProgress => true,
                TaskItemStatus.InProgress when next == TaskItemStatus.Done => true,
                _ => false
            };
        }

        private static bool IsTaskTitleUniqueConstraintViolation(DbUpdateException exception)
        {
            return exception.InnerException is SqliteException sqliteException &&
                sqliteException.SqliteExtendedErrorCode == SqliteUniqueConstraintErrorCode &&
                exception.Entries.Any(e => e.Entity is TaskItem);
        }
    }
}
