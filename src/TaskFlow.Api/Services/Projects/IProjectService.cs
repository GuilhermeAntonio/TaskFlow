using TaskFlow.Api.Contracts.Projects;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Services.Projects
{
    public interface IProjectService
    {
        Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken);

        Task<IReadOnlyList<ProjectResponse>> ListAsync(ProjectStatus? status, CancellationToken cancellationToken);

        Task<ProjectResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ProjectResponse> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken);
    }
}
