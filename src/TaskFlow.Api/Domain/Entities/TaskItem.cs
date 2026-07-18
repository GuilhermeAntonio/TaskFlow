using System;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        // Internal normalized field for uniqueness within a project
        public string NormalizedTitle { get; set; } = null!;
    }
}
