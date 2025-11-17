using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری کاربر
/// </summary>
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(TaskManagerDbContext context, IAuthService authService) : base(context, authService)
    {
    }

    public async Task<bool> IsExist(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
        return result;
    }
}