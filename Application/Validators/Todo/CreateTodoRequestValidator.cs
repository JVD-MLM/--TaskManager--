using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;

namespace TaskManager.Application.Validators.Todo;

/// <summary>
///     وليديتور ساخت تسک
/// </summary>
public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("عنوان نمی‌تواند خالی باشد");

        RuleFor(x => x.ProjectRef)
            .MustAsync(async (id, cancellationToken) => await projectRepository.IsExist(id, cancellationToken))
            .WithMessage("پروژه مورد نظر یافت نشد");

        RuleFor(x => x.UserRef)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("کاربر یافت نشد");
    }
}