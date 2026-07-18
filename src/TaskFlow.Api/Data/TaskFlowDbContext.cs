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
            ApplyAuditableRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditableRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditableRules()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Project || e.Entity is TaskItem)
                .ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity is Project p)
                {
                    if (entry.State == EntityState.Added)
                    {
                        p.CreatedAt = DateTime.UtcNow;
                    }
                    p.NormalizedName = Normalize(p.Name);
                }

                if (entry.Entity is TaskItem t)
                {
                    if (entry.State == EntityState.Added)
                    {
                        t.CreatedAt = DateTime.UtcNow;
                    }
                    t.NormalizedTitle = Normalize(t.Title);
                }
            }
        }

        private static string Normalize(string value)
        {
            return value?.Trim().ToLowerInvariant() ?? string.Empty;
        }
    }
}
