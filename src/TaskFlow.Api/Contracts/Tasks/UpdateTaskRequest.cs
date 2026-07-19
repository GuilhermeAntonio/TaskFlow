using System.Text.Json.Serialization;
using TaskFlow.Api.Contracts;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Contracts.Tasks
{
    public class UpdateTaskRequest
    {
        [JsonPropertyName("title")]
        public OptionalField<string> Title { get; set; }

        [JsonPropertyName("description")]
        public OptionalField<string?> Description { get; set; }

        [JsonPropertyName("status")]
        public OptionalField<TaskItemStatus> Status { get; set; }

        [JsonPropertyName("priority")]
        public OptionalField<TaskPriority> Priority { get; set; }
    }
}
