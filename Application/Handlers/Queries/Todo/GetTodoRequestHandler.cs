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
///     هندلر دريافت تسک
/// </summary>
public class GetTodoRequestHandler : IRequestHandler<GetTodoRequest, ApiResponse<GetTodoRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<GetTodoRequest> _validator;

    public GetTodoRequestHandler(IMapper mapper, ITodoRepository todoRepository, IValidator<GetTodoRequest> validator)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<GetTodoRequestResponse>> Handle(GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetTodoRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var todo = await _todoRepository.GetAsync(request.Id, cancellationToken);

        var result = _mapper.Map<TodoDto>(todo);

        return new ApiResponse<GetTodoRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetTodoRequestResponse
            {
                Item = result
            }
        };
    }
}