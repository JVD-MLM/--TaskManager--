using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Requests.Queries.Todo;

namespace TaskManager.Api.Controllers.Todo;

/// <summary>
///     کنترلر تسک
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     ایجاد تسک
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("create-todo")]
    [Description("ایجاد تسک")]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateTodoRequest
        {
            Title = request.Title,
            Description = request.Description,
            DeadLine = request.DeadLine,
            NeedApprove = request.NeedApprove,
            ProjectRef = request.ProjectRef,
            UserRef = request.UserRef
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت تسک
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-todo")]
    [Description("دريافت تسک")]
    public async Task<IActionResult> GetTodo([FromQuery] GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTodoRequest
        {
            Id = request.Id
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت همه تسک ها
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-all-todos")]
    [Description("دريافت همه تسک ها")]
    public async Task<IActionResult> GetAllTodos(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllTodosRequest());

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت همه تسک ها با فيلتر
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-all-todos-by-filter")]
    [Description("دريافت همه تسک ها با فيلتر")]
    public async Task<IActionResult> GetAllTodosByFilter([FromQuery] GetAllTodosByFilterRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllTodosByFilterRequest
        {
            Title = request.Title,
            IsComplete = request.IsComplete,
            Page = request.Page,
            PageSize = request.PageSize
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت همه تسك ها بر اساس پروژه
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-all-todos-by-project")]
    [Description("دريافت همه تسک ها بر اساس پروژه")]
    public async Task<IActionResult> GetAllTodosByProject([FromQuery] GetAllTodosByProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllTodosByProjectRequest
        {
            ProjectId = request.ProjectId
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     ويرايش تسک
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("edit-todo")]
    [Description("ويرايش تسک")]
    public async Task<IActionResult> EditTodo([FromBody] UpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateTodoRequest
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            IsComplete = request.IsComplete,
            DeadLine = request.DeadLine,
            NeedApprove = request.NeedApprove,
            ProjectRef = request.ProjectRef,
            UserRef = request.UserRef,
            IsApproved = request.IsApproved
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     حذف تسک
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("delete-todo")]
    [Description("حذف تسک")]
    public async Task<IActionResult> DeleteTodo([FromBody] DeleteTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteTodoRequest
        {
            Id = request.Id
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     تاييد تسک
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("approve-todo")]
    [Description("تاييد تسک")]
    public async Task<IActionResult> ApproveTodo([FromBody] ApproveTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ApproveTodoRequest
        {
            Id = request.Id
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     انجام تسک
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("done-todo")]
    [Description("انجام تسک")]
    public async Task<IActionResult> DoneTodo([FromBody] DoneTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DoneTodoRequest
        {
            Id = request.Id
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    ///     دريافت همه تسک های کاربر
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-all-todos-by-user")]
    [Description("دريافت همه تسک های کاربر")]
    public async Task<IActionResult> GetAllTodosByUser([FromQuery] GetAllTodosByUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllTodosByUserRequest
        {
            Id = request.Id
        });

        if (response.Status.HasError) return BadRequest(response);

        return Ok(response);
    }
}