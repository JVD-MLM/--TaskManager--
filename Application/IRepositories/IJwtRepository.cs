using TaskManager.Domain.Entities.Jwt;

namespace TaskManager.Application.IRepositories;

/// <summary>
///     اینترفیس JWT
/// </summary>
public interface IJwtRepository
{
    Task RevokeToken(RevokedToken token);
}