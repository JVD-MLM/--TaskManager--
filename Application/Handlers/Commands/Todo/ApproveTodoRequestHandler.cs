using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Commands.Todo;

/// <summary>
///     هندلر تاييد تسک
/// </summary>
public class ApproveTodoRequestHandler : IRequestHandler<ApproveTodoRequest, ApiResponse<ApproveTodoRequestResponse>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<ApproveTodoRequest> _validator;

    public ApproveTodoRequestHandler(ITodoRepository todoRepository, IValidator<ApproveTodoRequest> validator)
    {
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<ApproveTodoRequestResponse>> Handle(ApproveTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<ApproveTodoRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Result = null
            };

        await _todoRepository.ApproveAsync(request.Id, cancellationToken);

        return new ApiResponse<ApproveTodoRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = null
        };
    }
}