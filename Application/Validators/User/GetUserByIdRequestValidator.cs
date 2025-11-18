using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.User;

namespace TaskManager.Application.Validators.User;

/// <summary>
///     وليديتور دریافت کاربر با آی دی
/// </summary>
public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");
    }
}