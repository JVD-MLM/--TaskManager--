using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Commands.Todo;

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
}