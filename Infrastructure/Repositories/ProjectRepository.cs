using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;
using TaskManager.Domain.Entities.Project;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری پروژه
/// </summary>
public class ProjectRepository : BaseRepository, IProjectRepository
{
    public ProjectRepository(TaskManagerDbContext context, IAuthService authService) : base(context, authService)
    {
    }

    public async Task<bool> IsExist(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Projects.AnyAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Project> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Projects.Include(x => x.Users).Include(x => x.Todos)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Projects.ToListAsync(cancellationToken);
        return result;
    }

    public async Task UpdateAsync(Project project, CancellationToken cancellationToken)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await GetAsync(id, cancellationToken);
        project.SetDelete();
        await UpdateAsync(project, cancellationToken);
    }

    public async Task<List<Project>> GetAllProjectsByUser(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Projects.ToListAsync(cancellationToken);
        return result;
    }

    public async Task<List<Project>> GetAllByFilterAsync(string? title, int? isComplete, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _context.Projects.AsQueryable();

        if (!string.IsNullOrEmpty(title))
            query = query.Where(x => x.Title.Contains(title)); // فيلتر عنوان

        if (isComplete == 0)
            query = query.Where(x => x.IsComplete == false);
        else if (isComplete == 1) query = query.Where(x => x.IsComplete == true); // فيلتر كامل شده / نشده

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return result;
    }
}