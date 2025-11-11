using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Queries.Todo;

/// <summary>
///     درخواست دريافت تسک
/// </summary>
public class GetTodoRequest : IRequest<ApiResponse<GetTodoRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }
}