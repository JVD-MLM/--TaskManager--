using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Handlers.Commands.Project;

/// <summary>
///     هندلر حذف پروژه
/// </summary>
public class
    DeleteProjectRequestHandler : IRequestHandler<DeleteProjectRequest, ApiResponse<DeleteProjectRequestResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<DeleteProjectRequest> _validator;

    public DeleteProjectRequestHandler(IProjectRepository projectRepository, IValidator<DeleteProjectRequest> validator)
    {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<DeleteProjectRequestResponse>> Handle(DeleteProjectRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<DeleteProjectRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        await _projectRepository.DeleteAsync(request.Id, cancellationToken);

        return new ApiResponse<DeleteProjectRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = null
        };
    }
}