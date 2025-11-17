using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Authentication;

namespace TaskManager.Application.Validators.Authentication;

/// <summary>
///     ولیدیتور درخواست ثبت نام کاربر
/// </summary>
public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator(IUserRepository userRepository)
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

        RuleFor(x => x.RePassword)
            .NotEmpty().WithMessage("تکرار رمز عبور الزامی است")
            .Equal(x => x.Password).WithMessage("رمز عبور و تکرار آن مطابقت ندارند");

        RuleFor(x => x.ParentRef)
            .MustAsync(async (id, cancellationToken) =>
            {
                if (id == null)
                    return true; // چون والد اختیاری است

                return await userRepository.IsExist(id.Value, cancellationToken);
            })
            .WithMessage("کاربر والد یافت نشد");
    }
}