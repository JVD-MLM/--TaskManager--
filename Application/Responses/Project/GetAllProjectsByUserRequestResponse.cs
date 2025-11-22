using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Responses.Project;

/// <summary>
///     پاسخ دريافت همه پروژه های کاربر
/// </summary>
public class GetAllProjectsByUserRequestResponse
{
    /// <summary>
    ///     لیست Dto پروژه ها
    /// </summary>
    public List<ProjectDto> Items { get; set; }
}