using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Queries.Todo;

/// <summary>
///      درخواست دريافت همه تسک ها
/// </summary>
public class GetAllTodosRequest : IRequest<ApiResponse<GetAllTodosRequestResponse>>
{
}