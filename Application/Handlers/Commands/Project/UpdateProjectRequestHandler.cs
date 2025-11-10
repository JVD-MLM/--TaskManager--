using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Handlers.Commands.Project;

/// <summary>
///     هندلر ويرايش پروژه
/// </summary>
public class
    UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, ApiResponse<UpdateProjectRequestResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<UpdateProjectRequest> _validator;

    public UpdateProjectRequestHandler(IProjectRepository projectRepository, IValidator<UpdateProjectRequest> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<UpdateProjectRequestResponse>> Handle(UpdateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<UpdateProjectRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var project = await _projectRepository.GetAsync(request.Id, cancellationToken);

        project.Update(request.Title, request.Description, request.IsComplete);

        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new ApiResponse<UpdateProjectRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = null
        };
    }
}