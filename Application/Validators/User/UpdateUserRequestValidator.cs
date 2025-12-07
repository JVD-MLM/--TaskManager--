using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.User;

namespace TaskManager.Application.Validators.User;

/// <summary>
///     ولیدیتور ویرایش کاربر
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");

        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است")
            .Matches(@"^\d{10}$").WithMessage("کد ملی باید 10 رقم باشد")
            .MustAsync(async (request, nationalCode, cancellationToken) =>
            {
                var existingUser = await userRepository.GetAsync(nationalCode, cancellationToken);

                if (existingUser == null)
                    return true; // کد ملی تکراری نیست

                return existingUser.Id == request.Id; // اگر همان کاربر است، درست است، در غیر این صورت خطا
            })
            .WithMessage("کد ملی تکراری است");

        RuleFor(x => x.ParentRef)
            .MustAsync(async (id, cancellationToken) =>
            {
                if (id == null)
                    return true; // چون والد اختیاری است

                return await userRepository.IsExist(id.Value, cancellationToken);
            })
            .WithMessage("کاربر والد یافت نشد");

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
            .When(x => !string.IsNullOrWhiteSpace(x.Password));

        RuleFor(x => x.RePassword)
            .Equal(x => x.Password).WithMessage("رمز عبور و تکرار آن مطابقت ندارند")
            .When(x => !string.IsNullOrWhiteSpace(x.Password));
    }
}