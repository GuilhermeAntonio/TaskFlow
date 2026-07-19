using System.Text.Json.Serialization;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Contracts.Tasks
{
    public class TaskResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("projectId")]
        public Guid ProjectId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status")]
        public TaskItemStatus Status { get; set; }

        [JsonPropertyName("priority")]
        public TaskPriority Priority { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("completedAt")]
        public DateTime? CompletedAt { get; set; }

        public static TaskResponse FromEntity(TaskItem entity)
        {
            return new TaskResponse
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Priority = entity.Priority,
                CreatedAt = entity.CreatedAt,
                CompletedAt = entity.CompletedAt
            };
        }
    }
}
