using System.Text.Json.Serialization;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Contracts.Projects
{
    public class ProjectResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status")]
        public ProjectStatus Status { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        public static ProjectResponse FromEntity(Project project)
        {
            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                CreatedAt = project.CreatedAt
            };
        }
    }
}
