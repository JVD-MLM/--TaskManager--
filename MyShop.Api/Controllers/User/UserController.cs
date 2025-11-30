using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Commands.User;
using TaskManager.Application.Requests.Queries.User;

namespace TaskManager.Api.Controllers.User;

/// <summary>
///     کنترلر کاربر
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     دريافت کاربر با آی دی
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-user-by-id")]
    [Description("دريافت کاربر با آی دی")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserByIdRequest
        {
            Id = request.Id
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت کاربر با کد ملی
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-user-by-national-code")]
    [Description("دريافت کاربر با کد ملی")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserByNationalCodeRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserByNationalCodeRequest
        {
            NationalCode = request.NationalCode
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت همه کاربر ها
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-all-users")]
    [Description("دريافت همه کاربر ها")]
    public async Task<IActionResult> GetAllTodos(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllUsersRequest());

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     ويرايش کاربر
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    //[Authorize]
    [HttpPost("edit-user")]
    [Description("ويرايش کاربر")]
    public async Task<IActionResult> EditUser([FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateUserRequest
        {
            Id = request.Id,
            IsAdmin = request.IsAdmin,
            LastName = request.LastName,
            FirstName = request.FirstName,
            NationalCode = request.NationalCode,
            ParentRef = request.ParentRef,
            Gender = request.Gender,
            IsActive = request.IsActive,
            IsBlocked = request.IsBlocked,
            Password = request.Password,
            RePassword = request.RePassword
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }
}