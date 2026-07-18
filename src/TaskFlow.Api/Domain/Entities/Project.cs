using System;
using System.Collections.Generic;
using TaskFlow.Api.Domain.Enums;

namespace TaskFlow.Api.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.active;
        public DateTime CreatedAt { get; set; }

        // Internal normalized field for uniqueness (not part of public contract)
        public string NormalizedName { get; set; } = null!;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
