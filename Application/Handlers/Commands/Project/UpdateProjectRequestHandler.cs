using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.Handlers.Commands.Project;

/// <summary>
///     هندلر ويرايش پروژه
/// </summary>
public class
    UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, ApiResponse<UpdateProjectRequestResponse>>
{
    private readonly IAuthService _authService;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateProjectRequest> _validator;


    public UpdateProjectRequestHandler(IProjectRepository projectRepository, IValidator<UpdateProjectRequest> validator,
        IAuthService authService, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _validator = validator;
        _authService = authService;
        _userRepository = userRepository;
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
                Result = null
            };

        var project = await _projectRepository.GetAsync(request.Id, cancellationToken);

        project.Update(request.Title, request.Description, request.IsComplete);

        var isAdmin = _authService.CurrentUserIsInRole("Admin");

        if (isAdmin)
        {
            List<Guid> assignedUserIds;

            assignedUserIds = request.UserRefs;

            var users = new List<ApplicationUser>();

            foreach (var id in assignedUserIds)
            {
                var user = await _userRepository.GetAsync(id, cancellationToken);
                users.Add(user);
            }

            project.Users = users;
        }

        await _projectRepository.UpdateAsync(project, cancellationToken);

        return new ApiResponse<UpdateProjectRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = null
        };
    }
}