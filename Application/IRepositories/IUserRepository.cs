using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.IRepositories;

/// <summary>
///     اینترفیس کاربر
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     وجود داشتن / نداشتن
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsExist(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     وجود داشتن / نداشتن
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsExist(string nationalCode, CancellationToken cancellationToken);

    /// <summary>
    ///     دریافت کاربر با آی دی
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ApplicationUser> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     دریافت کاربر با کد ملی
    /// </summary>
    /// <param name="nationalCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ApplicationUser> GetAsync(string nationalCode, CancellationToken cancellationToken);

    /// <summary>
    ///     دریافت همه کاربر ها
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     ويرايش کاربر
    /// </summary>
    /// <returns></returns>
    Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);
}