using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("sign-up")]
    [Description("ثبت نام")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new SignUpRequest
        {
            NationalCode = request.NationalCode,
            Password = request.Password,
            RePassword = request.RePassword,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            ParentRef = request.ParentRef,
            Role = request.Role
        });

        if (response.Status.HasError) return BadRequest(response);

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
            NationalCode = request.NationalCode,
            Password = request.Password
        });

        if (response.Status.HasError) return BadRequest(response);

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

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }
}