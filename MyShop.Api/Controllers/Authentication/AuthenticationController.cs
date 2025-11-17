using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Commands.Authentication;

namespace TaskManager.Api.Controllers.Authentication;

/// <summary>
///     کنترلر احراز هویت
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     ثبت نام کاربر
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("sign-up")]
    [Description("ثبت نام")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SignUpRequest
        {
            Email = request.Email,
            UserName = request.UserName,
            Password = request.Password,
            RePassword = request.RePassword,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            ParentRef = request.ParentRef
        });

        return Ok(response);
    }

    /// <summary>
    ///     ورود کاربر
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("sign-in")]
    [Description("ورود")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SignInRequest
        {
            Email = request.Email,
            Password = request.Password
        });

        return Ok(response);
    }

    /// <summary>
    ///     خروج کاربر
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("sign-out")]
    [Description("خروج")]
    public async Task<IActionResult> SignOut([FromBody] SignOutRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SignOutRequest
        {
            Token = request.Token
        });

        return Ok(response);
    }
}