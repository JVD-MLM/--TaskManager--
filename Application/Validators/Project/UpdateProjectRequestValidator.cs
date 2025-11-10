using FluentValidation;
using TaskManager.Application.Requests.Commands.Project;

namespace TaskManager.Application.Validators.Project;

/// <summary>
///     وليديتور ويرايش پروژه
/// </summary>
public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("عنوان نمی‌تواند خالی باشد");
    }
}