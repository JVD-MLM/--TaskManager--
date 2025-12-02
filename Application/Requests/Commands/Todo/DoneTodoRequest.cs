using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Commands.Todo;

/// <summary>
///     درخواست انجام تسک
/// </summary>
public class DoneTodoRequest : IRequest<ApiResponse<DoneTodoRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }
}