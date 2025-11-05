using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities.Identity;

/// <summary>
///     کاربر
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    ///     ادمین
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    ///     نام
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    ///     نام خانوادگی
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///     حذف شده / نشده
    /// </summary>
    public bool IsDeleted { get; protected set; }

    /// <summary>
    ///     تاریخ ایجاد
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     تاریخ ویرایش
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    ///     بلاک شده / نشده
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    ///     فعال / غیر فعال
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     جنسیت
    /// </summary>
    public UserGender Gender { get; set; }

    /// <summary>
    ///     متد حذف
    /// </summary>
    public void SetDelete()
    {
        IsDeleted = true;
    }
}

public enum UserGender
{
    /// <summary>
    ///     مرد
    /// </summary>
    [Description("مرد")] Male = 1,

    /// <summary>
    ///     زن
    /// </summary>
    [Description("زن")] Female = 2,
}