using TaskManager.Domain.BaseEntities;
using TaskManager.Domain.Entities.Identity;

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

    #region Methods

    /// <summary>
    ///     متد ويرايش
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="isComplete"></param>
    public void Update(string title, string? description, bool isComplete)
    {
        Title = title;
        Description = description;
        IsComplete = isComplete;
    }

    #endregion


    #region Relations

    /// <summary>
    ///     تسک ها
    /// </summary>
    public List<Todo.Todo> Todos { get; set; }

    /// <summary>
    ///     کاربر ها
    /// </summary>
    public List<ApplicationUser> Users { get; set; }

    #endregion
}