using MediatR;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Domain.Enums;
using TaskManager.Identity;

namespace TaskManager.Application.Requests.Commands.Authentication;

/// <summary>
///     درخواست ثبت نام کاربر
/// </summary>
public class SignUpRequest : IRequest<ApiResponse<SignUpRequestResponse>>
{
    /// <summary>
    ///     کد ملی
    /// </summary>
    public string NationalCode { get; set; }

    /// <summary>
    ///     رمز عبور
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     تکرار رمز عبور
    /// </summary>
    public string RePassword { get; set; }

    /// <summary>
    ///     نام
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     نام انوادگی
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     جنسیت
    /// </summary>
    public UserGender Gender { get; set; }

    /// <summary>
    ///     آی دی والد
    /// </summary>
    public Guid? ParentRef { get; set; }

    /// <summary>
    ///     نقش
    /// </summary>
    public UserRoles Role { get; set; }
}