using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.Project;

namespace TaskManager.Application.Requests.Queries.Project;

/// <summary>
///     درخواست دريافت همه پروژه ها با فيلتر
/// </summary>
public class GetAllProjectsByFilterRequest : IRequest<ApiResponse<GetAllProjectsByFilterRequestResponse>>
{
    public string? Title { get; set; }

    //public bool? IsComplete { get; set; } todo

    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}