using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskFlow.Api.Contracts.Projects
{
    public class CreateProjectRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "O campo name é obrigatório.")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
