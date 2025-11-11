using AutoMapper;
using MediatR;
using TaskManager.Application.DTOs.Todo;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Todo;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Handlers.Queries.Todo;

/// <summary>
///     هندلر دريافت همه تسک ها
/// </summary>
public class GetAllTodosRequestHandler : IRequestHandler<GetAllTodosRequest, ApiResponse<GetAllTodosRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosRequestHandler(IMapper mapper, ITodoRepository todoRepository)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
    }

    public async Task<ApiResponse<GetAllTodosRequestResponse>> Handle(GetAllTodosRequest request,
        CancellationToken cancellationToken)
    {
        var todos = await _todoRepository.GetAllAsync();

        var result = _mapper.Map<List<TodoDto>>(todos);

        return new ApiResponse<GetAllTodosRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetAllTodosRequestResponse
            {
                Items = result
            }
        };
    }
}