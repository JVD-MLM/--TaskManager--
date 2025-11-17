using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Domain.Entities.Identity;

/// <summary>
///     کاربر
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    ///     کد ملی
    /// </summary>
    public string NationalCode { get; set; }

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
    ///     تاریخ ایجاد
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     تاریخ ایجاد
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    ///     تاریخ ویرایش
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    ///     تاریخ ویرایش
    /// </summary>
    public Guid? ModifiedBy { get; set; }

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
    ///     آی دی والد
    /// </summary>
    public Guid? ParentRef { get; set; }

    #region Methods

    /// <summary>
    ///     متد حذف
    /// </summary>
    //public void SetDelete()
    //{
    //    IsDeleted = true;
    //}

    #endregion

    #region Relations

    /// <summary>
    ///     تسک ها
    /// </summary>
    public List<Todo.Todo> Todos { get; set; }

    /// <summary>
    ///     پروژه ها
    /// </summary>
    public List<Project.Project> Projects { get; set; }

    /// <summary>
    ///     والد
    /// </summary>
    public ApplicationUser? Parent { get; set; }

    /// <summary>
    ///     فرزندان
    /// </summary>
    public List<ApplicationUser>? Childs { get; set; }

    #endregion
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
    [Description("زن")] Female = 2
}