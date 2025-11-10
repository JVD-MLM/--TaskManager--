using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IServices;
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
    private readonly IAuthService _authService;

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) :
        base(options) // كانستراكتور براي فكتوري
    {
    }

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options, // كانستراكتور عادي
        IAuthService authenticationService) : base(options)
    {
        _authService = authenticationService;
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

    #region CreatedAt&By & ModifiedAt&By Set

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
        var userId = _authService.GetCurrentUserId();

        var baseEntityEntries = ChangeTracker.Entries()
            .Where(e =>
                e.Entity.GetType().BaseType?.IsGenericType == true &&
                e.Entity.GetType().BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in baseEntityEntries) // پر كردن مقادير براي بيس انتيتي
        {
            var entity = entry.Entity;
            var createdAtProp = entity.GetType().GetProperty("CreatedAt");
            var modifiedAtProp = entity.GetType().GetProperty("ModifiedAt");
            var createdByProp = entity.GetType().GetProperty("CreatedBy");
            var modifiedByProp = entity.GetType().GetProperty("ModifiedBy");

            if (entry.State == EntityState.Added)
            {
                createdAtProp?.SetValue(entity, now);
                if (userId.HasValue)
                    createdByProp?.SetValue(entity, userId.Value);
            }

            if (entry.State == EntityState.Modified)
            {
                modifiedAtProp?.SetValue(entity, now);
                if (userId.HasValue)
                    modifiedByProp?.SetValue(entity, userId.Value);
            }
        }

        var userEntries = ChangeTracker.Entries<ApplicationUser>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in userEntries) // پر كردن مقادير براي انتيتي يوزر
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