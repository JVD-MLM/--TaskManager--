using AutoMapper;
using MediatR;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Handlers.Queries.Project;

/// <summary>
///     هندلر دريافت همه پروژه ها
/// </summary>
public class
    GetAllProjectsRequestHandler : IRequestHandler<GetAllProjectsRequest, ApiResponse<GetAllProjectsRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsRequestHandler(IMapper mapper, IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    public async Task<ApiResponse<GetAllProjectsRequestResponse>> Handle(GetAllProjectsRequest request,
        CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);

        var result = _mapper.Map<List<ProjectDto>>(projects);

        return new ApiResponse<GetAllProjectsRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetAllProjectsRequestResponse
            {
                Items = result
            }
        };
    }
}