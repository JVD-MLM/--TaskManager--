using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.EntitiesConfigurations.Todo;

/// <summary>
///     کانفیگ انتیتی تسک
/// </summary>
public class TodoConfiguration : IEntityTypeConfiguration<Domain.Entities.Todo.Todo>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Todo.Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(128);
        builder.Property(x => x.Description).HasMaxLength(512);
        builder.HasOne(x => x.Project).WithMany(x => x.Todos).HasForeignKey(x => x.ProjectRef)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.User).WithMany(x => x.Todos).HasForeignKey(x => x.UserRef)
            .OnDelete(DeleteBehavior.NoAction);
    }
}