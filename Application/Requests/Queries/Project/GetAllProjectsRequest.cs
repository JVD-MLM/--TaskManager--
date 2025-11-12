using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Queries.Project;

/// <summary>
///     درخواست دريافت همه پروژه ها
/// </summary>
public class GetAllProjectsRequest : IRequest<ApiResponse<GetAllProjectsRequestResponse>>
{
}