using AutoMapper;
using MediatR;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Handlers.Queries.Project;

/// <summary>
///     هندلر دريافت همه پروژه ها با فيلتر
/// </summary>
public class GetAllProjectsByFilterRequestHandler : IRequestHandler<GetAllProjectsByFilterRequest,
    ApiResponse<GetAllProjectsByFilterRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsByFilterRequestHandler(IMapper mapper, IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    public async Task<ApiResponse<GetAllProjectsByFilterRequestResponse>> Handle(GetAllProjectsByFilterRequest request,
        CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllByFilterAsync(request.Title, request.IsComplete,
            request.Page.Value,
            request.PageSize.Value, cancellationToken);

        var result = _mapper.Map<List<ProjectDto>>(projects);

        return new ApiResponse<GetAllProjectsByFilterRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = new GetAllProjectsByFilterRequestResponse
            {
                Items = result
            }
        };
    }
}