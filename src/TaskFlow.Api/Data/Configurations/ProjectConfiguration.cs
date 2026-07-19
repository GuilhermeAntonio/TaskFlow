using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.Data.Converters;

namespace TaskFlow.Api.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired(false);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(p => p.CreatedAt)
                .HasConversion(UtcDateTimeConverters.Required)
                .IsRequired();

            builder.Property(p => p.NormalizedName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(p => p.NormalizedName)
                .IsUnique()
                .HasDatabaseName("IX_Projects_NormalizedName");

            builder.HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
