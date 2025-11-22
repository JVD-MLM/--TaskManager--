using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Responses.Project;

/// <summary>
///     پاسخ دريافت همه پروژه ها با فيلتر
/// </summary>
public class GetAllProjectsByFilterRequestResponse
{
    /// <summary>
    ///     لیست Dto پروژه ها
    /// </summary>
    public List<ProjectDto> Items { get; set; }
}