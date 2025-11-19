using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Queries.Todo;

/// <summary>
///     درخواست دريافت همه تسک های کاربر
/// </summary>
public class GetAllTodosByUserRequest : IRequest<ApiResponse<GetAllTodosByUserRequestResponse>>
{
    /// <summary>
    ///     آی دی
    /// </summary>
    public Guid Id { get; set; }
}