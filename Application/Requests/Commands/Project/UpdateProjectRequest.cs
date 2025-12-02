using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Commands.Project;

/// <summary>
///     درخواست ويرايش پروژه
/// </summary>
public class UpdateProjectRequest : IRequest<ApiResponse<UpdateProjectRequestResponse>>
{
    /// <summary>
    ///     آي دي
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     عنوان
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     توضیحات
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     کامل شده / نشده
    /// </summary>
    public bool IsComplete { get; set; }

    /// <summary>
    ///     آی دی کاربر
    /// </summary>
    public List<Guid> UserRefs { get; set; } = new();
}