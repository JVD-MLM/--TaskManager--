using MediatR;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Application.Responses.BaseResponses;

namespace TaskManager.Application.Requests.Commands.Authentication;

/// <summary>
///     درخواست ورود کاربر
/// </summary>
public class SignInRequest : IRequest<ApiResponse<SignInRequestResponse>>
{
    /// <summary>
    ///     ایمیل
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     رمز عبور
    /// </summary>
    public string Password { get; set; }
}