using TaskManager.Domain.Entities.Jwt;

namespace TaskManager.Application.IRepositories;

/// <summary>
///     اینترفیس JWT
/// </summary>
public interface IJwtRepository
{
    /// <summary>
    ///     متد باطل كردن توكن
    /// </summary>
    /// <param name="token"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeToken(RevokedToken token, CancellationToken cancellationToken);
}