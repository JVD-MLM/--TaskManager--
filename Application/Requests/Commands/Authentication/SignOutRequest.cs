using MediatR;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Application.Responses.BaseResponses;

namespace TaskManager.Application.Requests.Commands.Authentication;

/// <summary>
///     درخواست خروج کاربر
/// </summary>
public class SignOutRequest : IRequest<ApiResponse<SignOutRequestResponse>>
{
    /// <summary>
    ///     توکن
    /// </summary>
    public string Token { get; set; } = string.Empty;
}