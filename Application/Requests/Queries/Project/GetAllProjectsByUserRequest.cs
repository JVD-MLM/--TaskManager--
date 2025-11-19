using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Queries.Project;

/// <summary>
///     درخواست دريافت همه پروژه های کاربر
/// </summary>
public class GetAllProjectsByUserRequest : IRequest<ApiResponse<GetAllProjectsByUserRequestResponse>>
{
    /// <summary>
    ///     آی دی
    /// </summary>
    public Guid Id { get; set; }
}