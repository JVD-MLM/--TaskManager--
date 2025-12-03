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
///     هندلر دريافت همه پروژه های کاربر
/// </summary>
public class GetAllProjectsByUserRequestHandler : IRequestHandler<GetAllProjectsByUserRequest,
    ApiResponse<GetAllProjectsByUserRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<GetAllProjectsByUserRequest> _validator;

    public GetAllProjectsByUserRequestHandler(IMapper mapper, IValidator<GetAllProjectsByUserRequest> validator,
        IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _validator = validator;
        _projectRepository = projectRepository;
    }

    public async Task<ApiResponse<GetAllProjectsByUserRequestResponse>> Handle(GetAllProjectsByUserRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetAllProjectsByUserRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Result = null
            };

        var projects = await _projectRepository.GetAllProjectsByUser(request.Id, cancellationToken);

        var result = _mapper.Map<List<ProjectDto>>(projects);

        return new ApiResponse<GetAllProjectsByUserRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = new GetAllProjectsByUserRequestResponse
            {
                Items = result
            }
        };
    }
}