using TaskManager.Application.IServices;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری پایه
/// </summary>
public class BaseRepository
{
    protected readonly IAuthService _authService;
    protected readonly TaskManagerDbContext _context;

    public BaseRepository(TaskManagerDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }
}