using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Commands.Todo;

/// <summary>
///     درخواست حذف تسک
/// </summary>
public class DeleteTodoRequest : IRequest<ApiResponse<DeleteTodoRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }
}