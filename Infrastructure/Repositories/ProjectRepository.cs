using Microsoft.EntityFrameworkCore;
using TaskManager.Application.IRepositories;
using TaskManager.Domain.Entities.Project;

namespace TaskManager.Infrastructure.Repositories;

/// <summary>
///     ریپازیتوری پروژه
/// </summary>
public class ProjectRepository : BaseRepository, IProjectRepository
{
    public ProjectRepository(TaskManagerDbContext context) : base(context)
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
        var result = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public Task<List<Project>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Project>> GetAllByFilterAsync()
    {
        throw new NotImplementedException();
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
}