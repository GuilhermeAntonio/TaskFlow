using TaskFlow.Api.Contracts.Tasks;

namespace TaskFlow.Api.Services.Tasks
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken);

        Task<IReadOnlyList<TaskResponse>> ListAsync(Guid projectId, TaskFlow.Api.Domain.Enums.TaskItemStatus? status, TaskFlow.Api.Domain.Enums.TaskPriority? priority, CancellationToken cancellationToken);

        Task<TaskResponse> PatchAsync(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
