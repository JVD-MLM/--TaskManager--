using TaskManager.Domain.Entities.Project;

namespace TaskManager.Application.IRepositories;

/// <summary>
///     اینترفیس پروژه
/// </summary>
public interface IProjectRepository
{
    /// <summary>
    ///     ایجاد پروژه
    /// </summary>
    /// <returns></returns>
    Task AddAsync(Project project, CancellationToken cancellationToken);

    Task<Project> GetAsync();

    Task<List<Project>> GetAllAsync();

    Task<List<Project>> GetAllByFilterAsync();

    Task UpdateAsync();

    Task DeleteAsync();
}