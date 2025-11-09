using TaskManager.Domain.BaseEntities;

namespace TaskManager.Domain.Entities.Project;

/// <summary>
///     پروژه
/// </summary>
public class Project : BaseEntity<Guid>
{
    /// <summary>
    ///     عنوان
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     توضیحات
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     کامل شده / نشده
    /// </summary>
    public bool IsComplete { get; set; }


    #region Relations

    /// <summary>
    ///     تسک ها
    /// </summary>
    public List<Task.Task> Tasks { get; set; }

    #endregion
}