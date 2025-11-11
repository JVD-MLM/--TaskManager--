using AutoMapper;
using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Commands.Todo;

public class CreateTodoRequestHandler : IRequestHandler<CreateTodoRequest, ApiResponse<CreateTodoRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<CreateTodoRequest> _validator;

    public CreateTodoRequestHandler(IMapper mapper, ITodoRepository todoRepository,
        IValidator<CreateTodoRequest> validator)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<CreateTodoRequestResponse>> Handle(CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<CreateTodoRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var newTodo = _mapper.Map<Domain.Entities.Todo.Todo>(request);

        await _todoRepository.AddAsync(newTodo, cancellationToken);

        return new ApiResponse<CreateTodoRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new CreateTodoRequestResponse
            {
                Id = newTodo.Id
            }
        };
    }
}