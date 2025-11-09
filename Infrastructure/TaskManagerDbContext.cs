using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.BaseEntities;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Entities.Jwt;
using TaskManager.Domain.Entities.Project;
using Task = TaskManager.Domain.Entities.Task.Task;

namespace TaskManager.Infrastructure;

/// <summary>
///     کانتکست دیتابیس
/// </summary>
public class TaskManagerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(builder);
    }

    #region Tables

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<RevokedToken> RevokedTokens { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }

    #endregion

    #region CreatedAt & ModifiedAt Set

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var now = DateTime.UtcNow;

        var baseEntityEntries = ChangeTracker.Entries()
            .Where(e =>
                e.Entity.GetType().BaseType?.IsGenericType == true &&
                e.Entity.GetType().BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in baseEntityEntries)
        {
            var entity = entry.Entity;
            var createdAtProp = entity.GetType().GetProperty("CreatedAt");
            var modifiedAtProp = entity.GetType().GetProperty("ModifiedAt");

            if (entry.State == EntityState.Added && createdAtProp != null)
                createdAtProp.SetValue(entity, now);

            if (entry.State == EntityState.Modified && modifiedAtProp != null)
                modifiedAtProp.SetValue(entity, now);
        }

        var userEntries = ChangeTracker.Entries<ApplicationUser>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);


        foreach (var entry in userEntries)
        {
            var user = entry.Entity;

            if (entry.State == EntityState.Added)
            {
                user.CreatedAt = now;
                user.ModifiedAt = null;
            }
            else if (entry.State == EntityState.Modified)
            {
                user.ModifiedAt = now;
            }
        }
    }

    #endregion
}