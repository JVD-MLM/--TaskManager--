using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Queries.Todo;

/// <summary>
///      درخواست دريافت همه تسک ها با فیلتر
/// </summary>
public class GetAllTodosByFilterRequest : IRequest<ApiResponse<GetAllTodosByFilterRequestResponse>>
{
    /// <summary>
    ///     عنوان
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     کامل شده / نشده
    /// </summary>
    public int? IsComplete { get; set; }

    /// <summary>
    ///     شماره صفحه
    /// </summary>
    public int? Page { get; set; } = 1;

    /// <summary>
    ///     اندازه هر صفحه
    /// </summary>
    public int? PageSize { get; set; } = 10;
}