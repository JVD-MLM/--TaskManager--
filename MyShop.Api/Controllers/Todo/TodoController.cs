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
            ProjectRef = request.ProjectRef
        });

        return Ok(response);
    }

    [HttpGet("get-todo")]
    [Description("دريافت تسک")]
    public async Task<IActionResult> GetTodo([FromQuery] GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetTodoRequest
        {
            Id = request.Id
        });

        return Ok(response);
    }

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
            IsApproved = request.IsApproved
        });

        return Ok(response);
    }

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

        return Ok(response);
    }
}