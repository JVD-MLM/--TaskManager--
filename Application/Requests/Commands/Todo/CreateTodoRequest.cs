using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Todo;

namespace TaskManager.Application.Requests.Commands.Todo;

/// <summary>
///     درخواست ایجاد تسک
/// </summary>
public class CreateTodoRequest : IRequest<ApiResponse<CreateTodoRequestResponse>>
{
    /// <summary>
    ///     عنوان
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     توضیحات
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     تاریخ ددلاین
    /// </summary>
    public DateTime? DeadLine { get; set; }

    /// <summary>
    ///     نیاز به تایید
    /// </summary>
    public bool NeedApprove { get; set; }

    /// <summary>
    ///     آی دی پروژه
    /// </summary>
    public Guid ProjectRef { get; set; }

    /// <summary>
    ///     آی دی کاربر
    /// </summary>
    public Guid UserRef { get; set; }
}