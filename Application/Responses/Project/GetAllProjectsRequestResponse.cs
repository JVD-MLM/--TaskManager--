using TaskManager.Application.DTOs.Project;

namespace TaskManager.Application.Responses.Project;

public class GetAllProjectsRequestResponse
{
    public List<ProjectDto> Items { get; set; }
}