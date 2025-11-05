using TaskManager.Application.IRepositories;
using TaskManager.Domain.Entities.Jwt;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری JWT
/// </summary>
public class JwtRepository : BaseRepository, IJwtRepository
{
    public JwtRepository(TaskManagerDbContext context) : base(context)
    {
    }

    /// <summary>
    ///     ابطال توکن
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task RevokeToken(RevokedToken token)
    {
        await _context.RevokedTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }
}