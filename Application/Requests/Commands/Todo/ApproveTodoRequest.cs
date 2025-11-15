using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Commands.Todo;

/// <summary>
///     درخواست تاييد تسک
/// </summary>
public class ApproveTodoRequest : IRequest<ApiResponse<ApproveTodoRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }
}