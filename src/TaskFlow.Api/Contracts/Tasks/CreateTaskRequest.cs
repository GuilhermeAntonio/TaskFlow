using System.Text.Json.Serialization;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Contracts.Tasks
{
    public class CreateTaskRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("priority")]
        public TaskPriority Priority { get; set; }
    }
}
