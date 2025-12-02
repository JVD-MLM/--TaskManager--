using TaskManager.Domain.Entities.Todo;

namespace TaskManager.Application.IRepositories;

/// <summary>
///     اینترفیس تسک
/// </summary>
public interface ITodoRepository
{
    /// <summary>
    ///     وجود داشتن / نداشتن
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsExist(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     ایجاد تسک
    /// </summary>
    /// <returns></returns>
    Task AddAsync(Todo todo, CancellationToken cancellationToken);

    /// <summary>
    ///     دريافت تسک
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Todo> GetAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     دريافت همه تسک ها
    /// </summary>
    /// <returns></returns>
    Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     دريافت همه تسک ها با فيلتر
    /// </summary>
    /// <param name="title"></param>
    /// <param name="isComplete"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Todo>> GetAllByFilterAsync(string? title, int? isComplete, int page, int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    ///     دريافت همه تسك ها بر اساس پروژه
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Todo>> GetTodosByProject(Guid projectId, CancellationToken cancellationToken);

    /// <summary>
    ///     ويرايش تسک
    /// </summary>
    /// <returns></returns>
    Task UpdateAsync(Todo todo, CancellationToken cancellationToken);

    /// <summary>
    ///     حذف تسک
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     تاييد تسک
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ApproveAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     انجام تسک
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DoneAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     دریافت همه تسک های کاربر
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Todo>> GetAllTodosByUser(Guid id, CancellationToken cancellationToken);
}