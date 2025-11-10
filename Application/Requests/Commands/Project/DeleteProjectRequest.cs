using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Commands.Project;

/// <summary>
///     درخواست حذف پروژه
/// </summary
public class DeleteProjectRequest : IRequest<ApiResponse<DeleteProjectRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }
}