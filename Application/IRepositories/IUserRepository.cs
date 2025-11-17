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
}