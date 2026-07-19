using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Data.Configurations;

namespace TaskFlow.Api.Data
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<TaskItem> TaskItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ApplyPersistenceRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyPersistenceRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyPersistenceRules()
        {
            var entries = ChangeTracker.Entries()
                .Where(entry =>
                    (entry.Entity is Project || entry.Entity is TaskItem) &&
                    (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified))
                .ToList();

            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.Entity is Project project)
                {
                    if (entry.State == EntityState.Added)
                    {
                        project.CreatedAt = utcNow;
                    }

                    project.NormalizedName = Normalize(project.Name);
                }

                if (entry.Entity is TaskItem taskItem)
                {
                    if (entry.State == EntityState.Added)
                    {
                        taskItem.CreatedAt = utcNow;
                    }

                    taskItem.NormalizedTitle = Normalize(taskItem.Title);
                }
            }
        }

        private static string Normalize(string? value)
        {
            return value?.Trim().ToLowerInvariant() ?? string.Empty;
        }
    }
}
