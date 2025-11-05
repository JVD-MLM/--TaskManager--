using MediatR;
using TaskManager.Application.Responses.Authentication;

namespace TaskManager.Application.Requests.Commands.Authentication;

/// <summary>
///     درخواست خروج کاربر
/// </summary>
public class SignOutRequest : IRequest<SignOutRequestResponse>
{
    /// <summary>
    ///     توکن
    /// </summary>
    public string Token { get; set; } = string.Empty;
}