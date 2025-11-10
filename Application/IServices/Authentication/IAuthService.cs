namespace TaskManager.Application.IServices.Authentication;

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
}