using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Contracts.Tasks;
using TaskFlow.Api.Domain.Enums;
using TaskFlow.Api.Services.Tasks;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("/projetos/{projectId}/tarefas")]
        public async Task<IActionResult> CreateAsync(
            Guid projectId,
            [FromBody] CreateTaskRequest request,
            CancellationToken cancellationToken)
        {
            var task = await _taskService.CreateAsync(
                projectId,
                request,
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, task);
        }

        [HttpGet("/projetos/{projectId}/tarefas")]
        public async Task<IActionResult> ListAsync(Guid projectId, [FromQuery] string? status, [FromQuery] string? priority, CancellationToken cancellationToken)
        {
            var parsedStatus = ParseStatus(status);
            var parsedPriority = ParsePriority(priority);

            var tasks = await _taskService.ListAsync(projectId, parsedStatus, parsedPriority, cancellationToken);
            return Ok(tasks);
        }

        [HttpPatch("/tarefas/{id}")]
        public async Task<IActionResult> PatchAsync(Guid id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var task = await _taskService.PatchAsync(id, request, cancellationToken);
            return Ok(task);
        }

        [HttpDelete("/tarefas/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _taskService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        private static TaskItemStatus? ParseStatus(string? status)
        {
            if (status is null) return null;

            return status switch
            {
                "pending" => TaskItemStatus.Pending,
                "in_progress" => TaskItemStatus.InProgress,
                "done" => TaskItemStatus.Done,
                _ => throw new TaskFlow.Api.Errors.Exceptions.ValidationException(
                    "O filtro status possui um valor inválido.",
                    new Dictionary<string, string[]>
                    {
                        ["status"] = new[] { "O campo status deve ser pending, in_progress ou done." }
                    })
            };
        }

        private static TaskPriority? ParsePriority(string? priority)
        {
            if (priority is null) return null;

            return priority switch
            {
                "low" => TaskPriority.Low,
                "medium" => TaskPriority.Medium,
                "high" => TaskPriority.High,
                _ => throw new TaskFlow.Api.Errors.Exceptions.ValidationException(
                    "O filtro priority possui um valor inválido.",
                    new Dictionary<string, string[]>
                    {
                        ["priority"] = new[] { "O campo priority deve ser low, medium ou high." }
                    })
            };
        }
    }
}
