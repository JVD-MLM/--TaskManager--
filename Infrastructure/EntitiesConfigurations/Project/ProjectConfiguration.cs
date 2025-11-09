using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.EntitiesConfigurations.Project;

public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Project.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Project.Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(256);
        builder.Property(x => x.Description).HasMaxLength(512);
    }
}