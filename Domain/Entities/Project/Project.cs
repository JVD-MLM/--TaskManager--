using TaskManager.Domain.BaseEntities;

namespace TaskManager.Domain.Entities.Project;

/// <summary>
///     پروژه
/// </summary>
public class Project : BaseEntity<Guid>
{
    public Project(string title, string? description, bool isComplete)
    {
        Title = title;
        Description = description;
        IsComplete = isComplete;
    }

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

    #region Methods

    public void Update(string title, string? description, bool isComplete)
    {
        Title = title;
        Description = description;
        IsComplete = isComplete;
    }

    #endregion
}