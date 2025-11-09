using AutoMapper;
using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Handlers.Commands.Project;

/// <summary>
///     هندلر ایجاد پروژه
/// </summary>
public class
    CreateProjectRequestHandler : IRequestHandler<CreateProjectRequest, ApiResponse<CreateProjectRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<CreateProjectRequest> _validator;

    public CreateProjectRequestHandler(IMapper mapper, IValidator<CreateProjectRequest> validator,
        IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _validator = validator;
        _projectRepository = projectRepository;
    }

    public async Task<ApiResponse<CreateProjectRequestResponse>> Handle(CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<CreateProjectRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var newProject = _mapper.Map<Domain.Entities.Project.Project>(request);

        await _projectRepository.AddAsync(newProject, cancellationToken);

        return new ApiResponse<CreateProjectRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new CreateProjectRequestResponse
            {
                Id = newProject.Id
            }
        };
    }
}