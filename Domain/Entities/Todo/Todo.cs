using TaskManager.Domain.BaseEntities;

namespace TaskManager.Domain.Entities.Todo;

/// <summary>
///     تسک
/// </summary>
public class Todo : BaseEntity<Guid>
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

    /// <summary>
    ///     تاریخ ددلاین
    /// </summary>
    public DateTime? DeadLine { get; set; }

    /// <summary>
    ///     تایید شده / نشده
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    ///     نیاز به تایید
    /// </summary>
    public bool NeedApprove { get; set; }

    /// <summary>
    ///     تایید شده توسط
    /// </summary>
    public Guid? ApprovedBy { get; set; }

    /// <summary>
    ///     آی دی پروژه
    /// </summary>
    public Guid ProjectRef { get; set; }


    #region Relations

    /// <summary>
    ///     پروژه
    /// </summary>
    public Project.Project Project { get; set; }

    #endregion

    #region Methods

    public void Update(string title, string? description, bool isComplete, DateTime? deadLine, bool isApproved,
        bool needApprove)
    {
        Title = title;
        Description = description;
        IsComplete = isComplete;
        DeadLine = deadLine;
        IsApproved = isApproved;
        NeedApprove = needApprove;
    }

    #endregion
}