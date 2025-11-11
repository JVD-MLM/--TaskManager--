using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.EntitiesConfigurations.Todo;

public class TodoConfiguration : IEntityTypeConfiguration<Domain.Entities.Todo.Todo>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Todo.Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(256);
        builder.Property(x => x.Description).HasMaxLength(512);
        builder.HasOne(x => x.Project).WithMany(x => x.Todos).HasForeignKey(x => x.ProjectRef)
            .OnDelete(DeleteBehavior.NoAction);
    }
}