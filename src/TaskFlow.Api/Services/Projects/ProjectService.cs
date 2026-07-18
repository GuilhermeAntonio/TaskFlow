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
        private readonly TaskFlowDbContext _dbContext;

        public ProjectService(TaskFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var trimmedName = request.Name.Trim();
            if (string.IsNullOrWhiteSpace(trimmedName))
            {
                throw new ValidationException(
                    "O campo name deve conter pelo menos um caractere diferente de espaço.",
                    new Dictionary<string, string[]>
                    {
                        ["name"] = new[] { "O campo name deve conter pelo menos um caractere diferente de espaço." }
                    });
            }

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
            catch (DbUpdateException ex)
            {
                HandleUniqueConstraintConflict(ex);
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
            var project = await _dbContext.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (project is null)
            {
                throw new NotFoundException($"Projeto com id '{id}' não foi encontrado.");
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

            var project = await _dbContext.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (project is null)
            {
                throw new NotFoundException($"Projeto com id '{id}' não foi encontrado.");
            }

            if (request.Name.HasValue)
            {
                var trimmedName = request.Name.Value?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(trimmedName))
                {
                    throw new ValidationException(
                        "O campo name deve conter pelo menos um caractere diferente de espaço.",
                        new Dictionary<string, string[]>
                        {
                            ["name"] = new[] { "O campo name deve conter pelo menos um caractere diferente de espaço." }
                        });
                }

                var normalizedName = Normalize(trimmedName);
                if (!string.Equals(project.NormalizedName, normalizedName, StringComparison.Ordinal))
                {
                    await EnsureUniqueNameAsync(normalizedName, cancellationToken, project.Id);
                    project.Name = trimmedName;
                    project.NormalizedName = normalizedName;
                }
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
                        var hasInProgressTasks = project.Tasks.Any(t => t.Status == TaskItemStatus.InProgress);
                        if (hasInProgressTasks)
                        {
                            throw new BusinessRuleViolationException("Não é possível arquivar o projeto enquanto existirem tarefas em progresso.");
                        }
                    }

                    project.Status = status;
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                HandleUniqueConstraintConflict(ex);
            }

            return ProjectResponse.FromEntity(project);
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
                throw new ConflictException("Já existe um projeto com este nome.");
            }
        }

        private static void HandleUniqueConstraintConflict(DbUpdateException exception)
        {
            if (exception.InnerException is not null && exception.InnerException.Message.Contains("IX_Projects_NormalizedName", StringComparison.OrdinalIgnoreCase))
            {
                throw new ConflictException("Já existe um projeto com este nome.");
            }

            throw exception;
        }
    }
}
