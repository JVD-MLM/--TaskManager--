using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Queries.Project;

public class GetAllProjectsRequest : IRequest<ApiResponse<GetAllProjectsRequestResponse>>
{
}