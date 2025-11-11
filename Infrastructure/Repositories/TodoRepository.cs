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
        var result = _context.Todos.ToListAsync();
        return result;
    }

    public async Task<List<Todo>> GetAllByFilterAsync(string? title, int? isComplete, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _context.Todos.AsQueryable();

        if (!string.IsNullOrEmpty(title))
            query = query.Where(x => x.Title.Contains(title)); // فيلتر عنوان

        if (isComplete == 0)
            query = query.Where(x => x.IsComplete == false);
        else if (isComplete == 1) query = query.Where(x => x.IsComplete == true); // فيلتر كامل شده / نشده

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return result;
    }

    public async Task UpdateAsync(Todo todo, CancellationToken cancellationToken)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var todo = await GetAsync(id, cancellationToken);
        todo.SetDelete();
        await UpdateAsync(todo, cancellationToken);
    }
}