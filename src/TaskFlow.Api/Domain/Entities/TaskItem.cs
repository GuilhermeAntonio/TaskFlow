using System;
using TaskFlow.Api.Domain.Enums;
using TaskStatus = TaskFlow.Api.Domain.Enums.TaskStatus;
using TaskPriority = TaskFlow.Api.Domain.Enums.TaskPriority;

namespace TaskFlow.Api.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.pending;
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }

        // Internal normalized field for uniqueness within a project
        public string NormalizedTitle { get; set; } = null!;
    }
}
