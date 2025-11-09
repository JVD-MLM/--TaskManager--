using TaskManager.Domain.BaseEntities;

namespace TaskManager.Domain.Entities.Task;

/// <summary>
///     تسک
/// </summary>
public class Task : BaseEntity<Guid>
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
    public Guid ApprovedBy { get; set; }

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
}