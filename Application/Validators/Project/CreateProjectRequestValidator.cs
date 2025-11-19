using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Project;

namespace TaskManager.Application.Validators.Project;

/// <summary>
///     وليديتور ساخت پروژه
/// </summary>
public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("عنوان نمی‌تواند خالی باشد");

        RuleFor(x => x.UserRefs)
            .MustAsync(async (userRefs, cancellationToken) =>
            {
                foreach (var id in userRefs)
                {
                    var exists = await userRepository.IsExist(id, cancellationToken);
                    if (!exists)
                        return false;
                }

                return true;
            })
            .WithMessage("یک یا چند کاربر یافت نشد");
    }
}