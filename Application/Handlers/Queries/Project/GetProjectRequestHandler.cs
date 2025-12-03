using AutoMapper;
using FluentValidation;
using MediatR;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Handlers.Queries.Project;

/// <summary>
///     هندلر دريافت پروژه
/// </summary>
public class GetProjectRequestHandler : IRequestHandler<GetProjectRequest, ApiResponse<GetProjectRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<GetProjectRequest> _validator;

    public GetProjectRequestHandler(IMapper mapper, IProjectRepository projectRepository,
        IValidator<GetProjectRequest> validator)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<GetProjectRequestResponse>> Handle(GetProjectRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetProjectRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Result = null
            };

        var project = await _projectRepository.GetAsync(request.Id, cancellationToken);

        var result = _mapper.Map<ProjectDto>(project);

        return new ApiResponse<GetProjectRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = new GetProjectRequestResponse
            {
                Item = result
            }
        };
    }
}