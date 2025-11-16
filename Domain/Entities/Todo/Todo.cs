using TaskManager.Domain.BaseEntities;
using TaskManager.Domain.Entities.Identity;

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
    public bool IsApproved { get; private set; }

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

    /// <summary>
    ///     آی دی کاربر
    /// </summary>
    public Guid UserRef { get; set; }


    #region Relations

    /// <summary>
    ///     پروژه
    /// </summary>
    public Project.Project Project { get; set; }

    /// <summary>
    ///     کاربر
    /// </summary>
    public ApplicationUser User { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     متد ویرایش
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="isComplete"></param>
    /// <param name="deadLine"></param>
    /// <param name="needApprove"></param>
    public void Update(string title, string? description, bool isComplete, DateTime? deadLine, bool needApprove)
    {
        Title = title;
        Description = description;
        IsComplete = isComplete;
        DeadLine = deadLine;
        NeedApprove = needApprove;
    }

    /// <summary>
    ///     متد مقدار دهی IsApproved
    /// </summary>
    public void SetIsApprove()
    {
        IsApproved = !NeedApprove;
    }

    /// <summary>
    ///     متد تایید
    /// </summary>
    public void SetApprove()
    {
        IsApproved = true;
    }

    #endregion
}