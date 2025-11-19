using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Commands.Project;

/// <summary>
///     درخواست ایجاد پروژه
/// </summary>
public class CreateProjectRequest : IRequest<ApiResponse<CreateProjectRequestResponse>>
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
    ///     آی دی کاربر
    /// </summary>
    public List<Guid> UserRefs { get; set; } = new();
}