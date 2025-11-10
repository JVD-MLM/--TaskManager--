using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Project;

namespace TaskManager.Application.Validators.Project;

/// <summary>
///     وليديتور دريافت پروژه
/// </summary>
public class GetProjectRequestValidator : AbstractValidator<GetProjectRequest>
{
    public GetProjectRequestValidator(IProjectRepository projectRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await projectRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");
    }
}