namespace TaskManager.Domain.BaseEntities;

/// <summary>
///     انتیتی پایه
/// </summary>
/// <typeparam name="T">نوع آی دی</typeparam>
public abstract class BaseEntity<T>
{
    /// <summary>
    ///     آی دی
    /// </summary>
    public T Id { get; set; }

    /// <summary>
    ///     حذف شده / نشده
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    ///     ایجاد شده توسط
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    ///     تاریخ ایجاد
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     ویرایش شده توسط
    /// </summary>
    public Guid? ModifiedBy { get; set; }

    /// <summary>
    ///     تاریخ ویرایش
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    ///     حذف شده توسط
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    ///     تاریخ حذف
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    ///     متد حذف
    /// </summary>
    public void SetDelete()
    {
        IsDeleted = true;
    }
}