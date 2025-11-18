using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;
using TaskManager.Domain.Entities.Identity;

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

    public async Task<bool> IsExist(string nationalCode, CancellationToken cancellationToken)
    {
        var result = await _context.Users.AnyAsync(x => x.NationalCode == nationalCode, cancellationToken);
        return result;
    }

    public async Task<ApplicationUser> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public async Task<ApplicationUser> GetAsync(string nationalCode, CancellationToken cancellationToken)
    {
        var result = await _context.Users.FirstOrDefaultAsync(x => x.NationalCode == nationalCode, cancellationToken);
        return result;
    }

    public async Task<List<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Users.ToListAsync(cancellationToken);
        return result;
    }

    public async Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}