using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Project;

namespace TaskManager.Application.Validators.Project;

/// <summary>
///     وليديتور ويرايش پروژه
/// </summary>
public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await projectRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");

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