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
///     هندلر دريافت همه تسک ها بر اساس پروژه
/// </summary>
public class GetAllTodosByProjectRequestHandler : IRequestHandler<GetAllTodosByProjectRequest,
    ApiResponse<GetAllTodosByProjectRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<GetAllTodosByProjectRequest> _validator;

    public GetAllTodosByProjectRequestHandler(IMapper mapper, ITodoRepository todoRepository,
        IValidator<GetAllTodosByProjectRequest> validator)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<GetAllTodosByProjectRequestResponse>> Handle(GetAllTodosByProjectRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetAllTodosByProjectRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var todos = await _todoRepository.GetTodosByProject(request.ProjectId, cancellationToken);

        var result = _mapper.Map<List<TodoDto>>(todos);

        return new ApiResponse<GetAllTodosByProjectRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetAllTodosByProjectRequestResponse
            {
                Items = result
            }
        };
    }
}