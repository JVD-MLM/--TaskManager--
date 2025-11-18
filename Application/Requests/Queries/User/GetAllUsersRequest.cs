using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;

namespace TaskManager.Application.Requests.Queries.User;

/// <summary>
///     درخواست دريافت همه کاربر ها
/// </summary>
public class GetAllUsersRequest : IRequest<ApiResponse<GetAllUsersRequestResponse>>
{
}