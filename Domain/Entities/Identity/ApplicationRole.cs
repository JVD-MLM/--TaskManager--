using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities.Identity;

/// <summary>
///     نقش کاربر
/// </summary>
public class ApplicationRole : IdentityRole<Guid>
{
}