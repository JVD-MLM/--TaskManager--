using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.IServices;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Commands.Todo;

/// <summary>
///     هندلر انجام تسک
/// </summary>
public class DoneTodoRequestHandler : IRequestHandler<DoneTodoRequest, ApiResponse<DoneTodoRequestResponse>>
{
    private readonly IAuthService _authService;
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<DoneTodoRequest> _validator;

    public DoneTodoRequestHandler(ITodoRepository todoRepository, IValidator<DoneTodoRequest> validator,
        IAuthService authService)
    {
        _todoRepository = todoRepository;
        _validator = validator;
        _authService = authService;
    }

    public async Task<ApiResponse<DoneTodoRequestResponse>> Handle(DoneTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<DoneTodoRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Result = null
            };

        var todo = await _todoRepository.GetAsync(request.Id, cancellationToken);

        var currentUserId = _authService.GetCurrentUserId();

        if (todo.UserRef == currentUserId)
        {
            await _todoRepository.DoneAsync(request.Id, cancellationToken);

            todo.DoneAt = DateTime.UtcNow;

            await _todoRepository.UpdateAsync(todo, cancellationToken);

            return new ApiResponse<DoneTodoRequestResponse>
            {
                Status = new StatusResponse(false),
                Result = null
            };
        }

        return new ApiResponse<DoneTodoRequestResponse>
        {
            Status = new StatusResponse(true)
            {
                Errors = new List<string> { "هر کاربر تنها می تواند تسک خود را به انجام شده تغییر دهد" }
            },
            Result = null
        };
    }
}