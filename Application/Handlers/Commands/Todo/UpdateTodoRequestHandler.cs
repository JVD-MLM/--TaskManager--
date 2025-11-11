using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Commands.Todo;

/// <summary>
///     هندلر ويرايش تسک
/// </summary>
public class UpdateTodoRequestHandler : IRequestHandler<UpdateTodoRequest, ApiResponse<UpdateTodoRequestResponse>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<UpdateTodoRequest> _validator;

    public UpdateTodoRequestHandler(ITodoRepository todoRepository, IValidator<UpdateTodoRequest> validator)
    {
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<UpdateTodoRequestResponse>> Handle(UpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<UpdateTodoRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var todo = await _todoRepository.GetAsync(request.Id, cancellationToken);

        todo.Update(request.Title, request.Description, request.IsComplete, request.DeadLine, request.IsApproved,
            request.NeedApprove);

        await _todoRepository.UpdateAsync(todo, cancellationToken);

        return new ApiResponse<UpdateTodoRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = null
        };
    }
}