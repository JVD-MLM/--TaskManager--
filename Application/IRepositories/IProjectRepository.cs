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

    /// <summary>
    ///     دريافت همه پروژه ها
    /// </summary>
    /// <returns></returns>
    Task<List<Project>> GetAllAsync();

    /// <summary>
    ///     دريافت همه پروژه ها با فيلتر
    /// </summary>
    /// <param name="title"></param>
    /// <param name="isComplete"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Project>> GetAllByFilterAsync(string? title, int page, int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    ///     ويرايش پروژه
    /// </summary>
    /// <param name="project"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Project project, CancellationToken cancellationToken);

    /// <summary>
    ///     حذف پروژه
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}