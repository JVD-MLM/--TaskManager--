namespace TaskManager.Application.IServices;

/// <summary>
///     اينترفيس دریافت کاربر جاری برای DbContext
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     آی دی کاربر جاری برای DbContext
    /// </summary>
    Guid? UserId { get; }
}