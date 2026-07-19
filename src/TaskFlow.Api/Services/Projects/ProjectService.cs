using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Contracts.Projects;
using TaskFlow.Api.Data;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Domain.Enums;
using TaskFlow.Api.Errors.Exceptions;

namespace TaskFlow.Api.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private const int ProjectNameMaxLength = 100;
        private const int SqliteUniqueConstraintErrorCode = 2067;

        private readonly TaskFlowDbContext _dbContext;

        public ProjectService(TaskFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var trimmedName = ValidateAndTrimName(request.Name);
            var normalizedName = Normalize(trimmedName);
            await EnsureUniqueNameAsync(normalizedName, cancellationToken);

            var project = new Project
            {
                Name = trimmedName,
                Description = request.Description,
                Status = ProjectStatus.Active,
                NormalizedName = normalizedName
            };

            _dbContext.Projects.Add(project);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
                when (IsProjectNameUniqueConstraintViolation(exception))
            {
                throw new ConflictException(
                    "Já existe um projeto com este nome.",
                    "project_name_conflict");
            }

            return ProjectResponse.FromEntity(project);
        }

        public async Task<IReadOnlyList<ProjectResponse>> ListAsync(ProjectStatus? status, CancellationToken cancellationToken)
        {
            var query = _dbContext.Projects.AsNoTracking().AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            var projects = await query.ToListAsync(cancellationToken);
            return projects.Select(ProjectResponse.FromEntity).ToList();
        }

        public async Task<ProjectResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    project => project.Id == id,
                    cancellationToken);

            if (project is null)
            {
                throw new NotFoundException(
                    $"Projeto com id '{id}' não foi encontrado.",
                    "project_not_found");
            }

            return ProjectResponse.FromEntity(project);
        }

        public async Task<ProjectResponse> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            if (!request.Name.HasValue && !request.Description.HasValue && !request.Status.HasValue)
            {
                throw new ValidationException(
                    "O corpo da requisição PATCH deve conter ao menos um campo para atualização.",
                    new Dictionary<string, string[]>
                    {
                        ["requestBody"] = new[] { "O corpo da requisição PATCH deve conter ao menos um campo para atualização." }
                    });
            }

            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(
                    project => project.Id == id,
                    cancellationToken);
            if (project is null)
            {
                throw new NotFoundException(
                    $"Projeto com id '{id}' não foi encontrado.",
                    "project_not_found");
            }

            if (request.Name.HasValue)
            {
                var trimmedName = ValidateAndTrimName(request.Name.Value);
                var normalizedName = Normalize(trimmedName);

                if (!string.Equals(
                    project.NormalizedName,
                    normalizedName,
                    StringComparison.Ordinal))
                {
                    await EnsureUniqueNameAsync(
                        normalizedName,
                        cancellationToken,
                        project.Id);
                }

                project.Name = trimmedName;
                project.NormalizedName = normalizedName;
            }

            if (request.Description.HasValue)
            {
                project.Description = request.Description.Value;
            }

            if (request.Status.HasValue)
            {
                var status = request.Status.Value;
                if (project.Status != status)
                {
                    if (status == ProjectStatus.Archived)
                    {
                        var hasInProgressTasks =
                            await _dbContext.TaskItems.AnyAsync(
                                task =>
                                    task.ProjectId == project.Id &&
                                    task.Status == TaskItemStatus.InProgress,
                                cancellationToken);

                        if (hasInProgressTasks)
                        {
                            throw new BusinessRuleViolationException(
                                "Não é possível arquivar o projeto enquanto existirem tarefas em progresso.",
                                "project_has_in_progress_tasks");
                        }
                    }
                    project.Status = status;
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
                when (IsProjectNameUniqueConstraintViolation(exception))
            {
                throw new ConflictException(
                    "Já existe um projeto com este nome.",
                    "project_name_conflict");
            }

            return ProjectResponse.FromEntity(project);
        }

        private static string ValidateAndTrimName(string? name)
        {
            var trimmedName = name?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(trimmedName))
            {
                throw new ValidationException(
                    "O campo name deve conter pelo menos um caractere diferente de espaço.",
                    new Dictionary<string, string[]>
                    {
                        ["name"] =
                        [
                            "O campo name deve conter pelo menos um caractere diferente de espaço."
                        ]
                    });
            }

            if (trimmedName.Length > ProjectNameMaxLength)
            {
                throw new ValidationException(
                    "O campo name deve possuir no máximo 100 caracteres.",
                    new Dictionary<string, string[]>
                    {
                        ["name"] =
                        [
                            "O campo name deve possuir no máximo 100 caracteres."
                        ]
                    });
            }

            return trimmedName;
        }

        private static string Normalize(string value)
        {
            return value.Trim().ToLowerInvariant();
        }

        private async Task EnsureUniqueNameAsync(string normalizedName, CancellationToken cancellationToken, Guid? currentProjectId = null)
        {
            var query = _dbContext.Projects.AsNoTracking().Where(p => p.NormalizedName == normalizedName);
            if (currentProjectId.HasValue)
            {
                query = query.Where(p => p.Id != currentProjectId.Value);
            }

            var exists = await query.AnyAsync(cancellationToken);
            if (exists)
            {
                throw new ConflictException(
                    "Já existe um projeto com este nome.",
                    "project_name_conflict");
            }
        }

        private static bool IsProjectNameUniqueConstraintViolation(
            DbUpdateException exception)
        {
            return exception.InnerException is SqliteException sqliteException &&
                sqliteException.SqliteExtendedErrorCode ==
                    SqliteUniqueConstraintErrorCode &&
                exception.Entries.Any(entry => entry.Entity is Project);
        }
    }
}
