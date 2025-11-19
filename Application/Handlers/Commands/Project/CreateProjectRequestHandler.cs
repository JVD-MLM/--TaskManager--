using AutoMapper;
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
///     هندلر ایجاد پروژه
/// </summary>
public class
    CreateProjectRequestHandler : IRequestHandler<CreateProjectRequest, ApiResponse<CreateProjectRequestResponse>>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateProjectRequest> _validator;

    public CreateProjectRequestHandler(IMapper mapper, IProjectRepository projectRepository,
        IUserRepository userRepository, IAuthService authService, IValidator<CreateProjectRequest> validator)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _authService = authService;
        _validator = validator;
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

        var isAdmin = _authService.CurrentUserIsInRole("Admin");

        var isManager = _authService.CurrentUserIsInRole("Manager");

        var currentUserId = _authService.GetCurrentUserId();

        List<Guid> assignedUserIds;

        if (isAdmin)
            // ادمین می‌تواند هر کاربری انتخاب کند
            assignedUserIds = request.UserRefs;
        else
            // مدیر است و مدیر فقط خودش را می‌تواند ثبت کند
            assignedUserIds = new List<Guid> { currentUserId!.Value };

        var newProject = _mapper.Map<Domain.Entities.Project.Project>(request);

        var users = new List<ApplicationUser>();

        foreach (var id in assignedUserIds)
        {
            var user = await _userRepository.GetAsync(id, cancellationToken);
            users.Add(user);
        }

        newProject.Users = users;

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