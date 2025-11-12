using FluentValidation;
using TaskManager.Application.Requests.Commands.Authentication;

namespace TaskManager.Application.Validators.Authentication;

/// <summary>
///     ولیدیتور درخواست ثبت نام کاربر
/// </summary>
public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("نام کاربری الزامی است")
            .MinimumLength(3).WithMessage("نام کاربری باید حداقل 3 کاراکتر باشد");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("ایمیل الزامی است")
            .EmailAddress().WithMessage("ایمیل وارد شده معتبر نیست");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد");
    }
}