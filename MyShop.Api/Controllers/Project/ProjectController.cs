using System.ComponentModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Requests.Queries.Project;

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

    [HttpGet("get-project")]
    [Description("دريافت پروژه")]
    public async Task<IActionResult> GetProject([FromQuery] GetProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProjectRequest
        {
            Id = request.Id
        });

        return Ok(response);
    }

    [Authorize]
    [HttpPost("edit-project")]
    [Description("ويرايش پروژه")]
    public async Task<IActionResult> EditProject([FromBody] UpdateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateProjectRequest
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            IsComplete = request.IsComplete
        });

        return Ok(response);
    }

    [Authorize]
    [HttpPost("delete-project")]
    [Description("حذف پروژه")]
    public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteProjectRequest
        {
            Id = request.Id
        });

        return Ok(response);
    }
}