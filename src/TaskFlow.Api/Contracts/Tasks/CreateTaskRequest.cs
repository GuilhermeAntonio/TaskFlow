using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Contracts.Tasks
{
    public class CreateTaskRequest
    {
        [Required(ErrorMessage = "O campo title é obrigatório.")]
        [StringLength(
            200,
            ErrorMessage = "O campo title deve possuir no máximo 200 caracteres.")]
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "O campo priority é obrigatório.")]
        [JsonPropertyName("priority")]
        public TaskPriority? Priority { get; set; }
    }
}
