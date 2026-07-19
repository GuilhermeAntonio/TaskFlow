using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskFlow.Api.Contracts.Projects
{
    public class CreateProjectRequest
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "O campo name é obrigatório.")]
        [StringLength(
            100,
            ErrorMessage = "O campo name deve possuir no máximo 100 caracteres.")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
