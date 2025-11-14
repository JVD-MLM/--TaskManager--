using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Queries.Todo;

/// <summary>
///     درخواست دريافت همه تسک ها بر اساس پروژه
/// </summary>
public class GetAllTodosByProjectRequest : IRequest<ApiResponse<GetAllTodosByProjectRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid ProjectId { get; set; }
}