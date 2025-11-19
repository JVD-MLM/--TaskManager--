using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Project;

namespace TaskManager.Application.Validators.Project;

/// <summary>
///     وليديتور دريافت همه پروژه های کاربر
/// </summary>
public class GetAllProjectsByUserRequestValidator : AbstractValidator<GetAllProjectsByUserRequest>
{
    public GetAllProjectsByUserRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("کاربر مورد نظر یافت نشد");
    }
}