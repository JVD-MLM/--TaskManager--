using FluentValidation;
using TaskManager.Application.Requests.Commands.Project;

namespace TaskManager.Application.Validators.Project;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("عنوان نمی‌تواند خالی باشد");
    }
}