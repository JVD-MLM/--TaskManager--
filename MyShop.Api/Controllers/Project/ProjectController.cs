using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Commands.Project;

namespace TaskManager.Api.Controllers.Project;

/// <summary>
///     کنترلر پروژه
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("create-project")]
    [Description("ایجاد پروژه")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateProjectRequest
        {
            Title = request.Title,
            Description = request.Description
        });

        return Ok(response);
    }
}