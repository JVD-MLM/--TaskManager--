namespace TaskManager.Application.DTOs.Todo;

/// <summary>
///     Dto تسک
/// </summary>
public class TodoDto
{
    /// <summary>
    ///     آی دی
    /// </summary>
    public Guid Id { get; set; }

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
    public string? DeadLine { get; set; }

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
    ///     ایجاد شده توسط
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    ///     تاریخ ایجاد
    /// </summary>
    public string CreatedAt { get; set; }

    /// <summary>
    ///     آی دی پروژه
    /// </summary>
    public Guid ProjectRef { get; set; }

    /// <summary>
    ///     آی دی کاربر
    /// </summary>
    public Guid UserRef { get; set; }
}