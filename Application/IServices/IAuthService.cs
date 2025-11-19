using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.IServices;

/// <summary>
///     اينترفيس احراز هويت
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     متد گرفتن آي دي كاربر جاري
    /// </summary>
    /// <returns></returns>
    public Guid? GetCurrentUserId();

    /// <summary>
    ///     متد گرفتن كاربر جاري
    /// </summary>
    /// <returns></returns>
    public Task<ApplicationUser?> GetCurrentUser(CancellationToken cancellationToken);

    /// <summary>
    ///     چک کردن نقش کاربر جاری
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public bool CurrentUserIsInRole(string role);
}