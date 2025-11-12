using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Responses.Project;

/// <summary>
///     پاسخ دريافت پروژه
/// </summary>
public class GetProjectRequestResponse
{
    /// <summary>
    /// Dto پروژه
    /// </summary>
    public ProjectDto Item { get; set; }
}