using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Data.Converters;

namespace TaskFlow.Api.Data.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("TaskItems");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .IsRequired(false);

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(t => t.Priority)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(t => t.CreatedAt)
                .HasConversion(UtcDateTimeConverters.Required)
                .IsRequired();

            builder.Property(t => t.CompletedAt)
                .HasConversion(UtcDateTimeConverters.Nullable)
                .IsRequired(false);

            builder.Property(t => t.NormalizedTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(t => new { t.ProjectId, t.NormalizedTitle })
                .IsUnique()
                .HasDatabaseName("IX_TaskItems_ProjectId_NormalizedTitle");
        }
    }
}
