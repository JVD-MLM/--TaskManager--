namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری پایه
/// </summary>
public class BaseRepository
{
    protected readonly TaskManagerDbContext _context;

    public BaseRepository(TaskManagerDbContext context)
    {
        _context = context;
    }
}