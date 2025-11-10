using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Project;

namespace TaskManager.Application.Validators.Project;

public class DeleteProjectRequestValidator:AbstractValidator<DeleteProjectRequest>
{
    public DeleteProjectRequestValidator(IProjectRepository projectRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await projectRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");
    }
}