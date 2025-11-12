using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Responses.Project;

/// <summary>
///     پاسخ دريافت همه پروژه ها
/// </summary>
public class GetAllProjectsRequestResponse
{
    /// <summary>
    /// لیست Dto پروژه ها
    /// </summary>
    public List<ProjectDto> Items { get; set; }
}