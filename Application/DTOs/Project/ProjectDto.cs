using TaskManager.Application.DTOs.Todo;
using TaskManager.Application.DTOs.User;

namespace TaskManager.Application.DTOs.Project;

/// <summary>
///     Dto پروژه
/// </summary>
public class ProjectDto
{
    /// <summary>
    ///     آی دی
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
    ///     ایجاد شده توسط
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    ///     تاریخ ایجاد
    /// </summary>
    public string CreatedAt { get; set; }

    /// <summary>
    ///     کاربر ها
    /// </summary>
    public List<UserDto>? Users { get; set; }

    /// <summary>
    ///     تسک ها
    /// </summary>
    public List<TodoDto>? Todos { get; set; }
}