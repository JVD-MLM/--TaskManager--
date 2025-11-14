using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Todo;

namespace TaskManager.Application.Validators.Todo;

/// <summary>
///     وليديتور دریافت همه تسک ها بر اساس پروژه
/// </summary>
public class GetAllTodosByProjectRequestValidator : AbstractValidator<GetAllTodosByProjectRequest>
{
    public GetAllTodosByProjectRequestValidator(IProjectRepository projectRepository)
    {
        RuleFor(x => x.ProjectId)
            .MustAsync(async (projectId, cancellationToken) =>
                await projectRepository.IsExist(projectId, cancellationToken))
            .WithMessage("پروژه مورد نظر يافت نشد");
    }
}