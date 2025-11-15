using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;
using TaskManager.Domain.Entities.Jwt;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری JWT
/// </summary>
public class JwtRepository : BaseRepository, IJwtRepository
{
    public JwtRepository(TaskManagerDbContext context, IAuthService authService) : base(context, authService)
    {
    }

    public async Task RevokeToken(RevokedToken token)
    {
        await _context.RevokedTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }
}