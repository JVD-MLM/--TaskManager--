using AutoMapper;
using FluentValidation;
using MediatR;
using TaskManager.Application.DTOs.Todo;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Queries.Todo;

/// <summary>
///     هندلر دريافت همه تسک های کاربر
/// </summary>
public class
    GetAllTodosByUserRequestHandler : IRequestHandler<GetAllTodosByUserRequest,
    ApiResponse<GetAllTodosByUserRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<GetAllTodosByUserRequest> _validator;

    public GetAllTodosByUserRequestHandler(IMapper mapper, ITodoRepository todoRepository,
        IValidator<GetAllTodosByUserRequest> validator)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<GetAllTodosByUserRequestResponse>> Handle(GetAllTodosByUserRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetAllTodosByUserRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var todos = await _todoRepository.GetAllTodosByUser(request.Id, cancellationToken);

        var result = _mapper.Map<List<TodoDto>>(todos);

        return new ApiResponse<GetAllTodosByUserRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetAllTodosByUserRequestResponse
            {
                Items = result
            }
        };
    }
}