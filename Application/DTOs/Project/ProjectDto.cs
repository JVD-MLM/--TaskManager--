namespace TaskManager.Application.DTOs.Project;

/// <summary>
/// Dto پروژه
/// </summary>
public class ProjectDto
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
    ///     کامل شده / نشده
    /// </summary>
    public bool IsComplete { get; set; }
}