using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.User;

namespace TaskManager.Application.Validators.User;

public class GetUserByNationalCodeRequestValidator : AbstractValidator<GetUserByNationalCodeRequest>
{
    public GetUserByNationalCodeRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.NationalCode)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");
    }
}