using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IRepositories;
using TaskManager.Domain.Entities.Todo;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری تسک
/// </summary>
public class TodoRepository : BaseRepository, ITodoRepository
{
    public TodoRepository(TaskManagerDbContext context) : base(context)
    {
    }

    public async Task<bool> IsExist(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Todos.AnyAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public async Task AddAsync(Todo todo, CancellationToken cancellationToken)
    {
        await _context.Todos.AddAsync(todo, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Todo> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public Task<List<Todo>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Todo>> GetAllByFilterAsync(string? title, int? isComplete, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Todo todo, CancellationToken cancellationToken)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}