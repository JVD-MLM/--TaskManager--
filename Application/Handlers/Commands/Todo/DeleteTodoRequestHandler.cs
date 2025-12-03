using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Commands.Todo;

/// <summary>
///     هندلر حذف تسک
/// </summary>
public class DeleteTodoRequestHandler : IRequestHandler<DeleteTodoRequest, ApiResponse<DeleteTodoRequestResponse>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<DeleteTodoRequest> _validator;

    public DeleteTodoRequestHandler(ITodoRepository todoRepository, IValidator<DeleteTodoRequest> validator)
    {
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<DeleteTodoRequestResponse>> Handle(DeleteTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<DeleteTodoRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Result = null
            };

        await _todoRepository.DeleteAsync(request.Id, cancellationToken);

        return new ApiResponse<DeleteTodoRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = null
        };
    }
}