using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.DTOs.User;

/// <summary>
///     Dto کاربر
/// </summary>
public class UserDto
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
}