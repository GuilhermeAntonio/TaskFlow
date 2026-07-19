using System.Text.Json.Serialization;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Contracts.Projects
{
    public class UpdateProjectRequest
    {
        [JsonPropertyName("name")]
        public OptionalField<string> Name { get; set; }

        [JsonPropertyName("description")]
        public OptionalField<string?> Description { get; set; }

        [JsonPropertyName("status")]
        public OptionalField<ProjectStatus> Status { get; set; }
    }
}
