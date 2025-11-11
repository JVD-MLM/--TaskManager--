using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Queries.Todo;

public class GetAllTodosRequest : IRequest<ApiResponse<GetAllTodosRequestResponse>>
{
}