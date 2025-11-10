using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Queries.Project;

public class GetProjectRequest : IRequest<ApiResponse<GetProjectRequestResponse>>
{
    public Guid Id { get; set; }
}