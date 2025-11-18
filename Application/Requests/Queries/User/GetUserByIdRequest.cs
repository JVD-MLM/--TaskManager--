using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;

namespace TaskManager.Application.Requests.Queries.User;

/// <summary>
///     درخواست دريافت کاربر با آی دی
/// </summary>
public class GetUserByIdRequest : IRequest<ApiResponse<GetUserByIdRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }
}