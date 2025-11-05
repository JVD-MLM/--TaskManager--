using MediatR;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.Requests.Commands.Authentication;

/// <summary>
///     درخواست ثبت نام کاربر
/// </summary>
public class SignUpRequest : IRequest<SignUpRequestResponse>
{
    /// <summary>
    ///     نام کاربری
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     رمز عبور
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     ایمیل
    /// </summary>
    public string Email { get; set; }

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
}