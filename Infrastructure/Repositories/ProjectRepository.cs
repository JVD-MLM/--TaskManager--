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

    public async Task AddAsync(Project project, CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(project, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Project> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Project>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Project>> GetAllByFilterAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }
}