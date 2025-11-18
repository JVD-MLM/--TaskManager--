using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.User;

namespace TaskManager.Application.Validators.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");

        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی نمی‌تواند خالی باشد.")
            .Matches(@"^\d{10}$").WithMessage("کد ملی باید ۱۰ رقم باشد.")
            .MustAsync(async (nationalCode, cancellationToken) =>
                !await userRepository.IsExist(nationalCode, cancellationToken))
            .WithMessage("این کد ملی قبلاً ثبت شده است.");

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