using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Requests.Commands.User;

/// <summary>
///     درخواست ويرايش کاربر
/// </summary>
public class UpdateUserRequest : IRequest<ApiResponse<UpdateUserRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    ///     رمز عبور
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    ///     تکرار رمز عبور
    /// </summary>
    public string? RePassword { get; set; }
}