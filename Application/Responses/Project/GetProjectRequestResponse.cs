using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Responses.Project;

/// <summary>
///     پاسخ دريافت پروژه
/// </summary>
public class GetProjectRequestResponse
{
    public ProjectDto ProjectDto { get; set; }
}