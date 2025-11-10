using TaskManager.Domain.Entities.Project;

namespace TaskManager.Application.IRepositories;

/// <summary>
///     اینترفیس پروژه
/// </summary>
public interface IProjectRepository
{
    /// <summary>
    ///     وجود داشتن / نداشتن
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsExist(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     ایجاد پروژه
    /// </summary>
    /// <param name="project"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(Project project, CancellationToken cancellationToken);

    /// <summary>
    ///     دريافت پروژه
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Project> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Project>> GetAllAsync();

    Task<List<Project>> GetAllByFilterAsync();

    /// <summary>
    ///     ويرايش پروژه
    /// </summary>
    /// <param name="project"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Project project, CancellationToken cancellationToken);

    Task DeleteAsync();
}