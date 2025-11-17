using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.EntitiesConfigurations.Project;

/// <summary>
///     کانفیگ انتیتی Project
/// </summary>
public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Project.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Project.Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(128);
        builder.Property(x => x.Description).HasMaxLength(512);
        builder.HasMany(x => x.Users).WithMany(x => x.Projects);
    }
}