using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IServices;
using TaskManager.Domain.BaseEntities;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Entities.Jwt;
using TaskManager.Domain.Entities.Project;
using TaskManager.Domain.Entities.Todo;

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
    public DbSet<Todo> Todos { get; set; }

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

        foreach (var entry in baseEntityEntries)
        {
            var entity = entry.Entity;
            var type = entity.GetType();

            var createdAtProp = type.GetProperty("CreatedAt");
            var modifiedAtProp = type.GetProperty("ModifiedAt");
            var createdByProp = type.GetProperty("CreatedBy");
            var modifiedByProp = type.GetProperty("ModifiedBy");

            var isDeletedProp = type.GetProperty("IsDeleted");
            var deletedAtProp = type.GetProperty("DeletedAt");
            var deletedByProp = type.GetProperty("DeletedBy");

            var isDeleted = (bool)(isDeletedProp?.GetValue(entity) ?? false);
            var wasDeletedBefore = entry.OriginalValues.GetValue<bool>("IsDeleted");

            if (!wasDeletedBefore && isDeleted)
            {
                deletedAtProp?.SetValue(entity, now);
                if (userId.HasValue)
                    deletedByProp?.SetValue(entity, userId);

                continue;
            }

            if (entry.State == EntityState.Added)
            {
                createdAtProp?.SetValue(entity, now);
                if (userId.HasValue)
                    createdByProp?.SetValue(entity, userId);
            }

            if (entry.State == EntityState.Modified)
            {
                modifiedAtProp?.SetValue(entity, now);
                if (userId.HasValue)
                    modifiedByProp?.SetValue(entity, userId);
            }
        }
    }

    #endregion
}