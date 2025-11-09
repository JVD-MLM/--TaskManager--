using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.EntitiesConfigurations.Task;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task.Task> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(256);
        builder.Property(x => x.Description).HasMaxLength(512);
        builder.HasOne(x => x.Project).WithMany(x => x.Tasks).HasForeignKey(x => x.ProjectRef)
            .OnDelete(DeleteBehavior.NoAction);
    }
}