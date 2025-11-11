using AutoMapper;
using MediatR;
using TaskManager.Application.DTOs.Todo;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Queries.Todo;

/// <summary>
///     هندلر دريافت همه تسک ها با فیلتر
/// </summary>
public class GetAllTodosByFilterRequestHandler : IRequestHandler<GetAllTodosByFilterRequest,
    ApiResponse<GetAllTodosByFilterRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosByFilterRequestHandler(IMapper mapper, ITodoRepository todoRepository)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
    }

    public async Task<ApiResponse<GetAllTodosByFilterRequestResponse>> Handle(GetAllTodosByFilterRequest request,
        CancellationToken cancellationToken)
    {
        var todos = await _todoRepository.GetAllByFilterAsync(request.Title, request.IsComplete,
            request.Page.Value,
            request.PageSize.Value, cancellationToken);

        var result = _mapper.Map<List<TodoDto>>(todos);

        return new ApiResponse<GetAllTodosByFilterRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetAllTodosByFilterRequestResponse
            {
                Items = result
            }
        };
    }
}