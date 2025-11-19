using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Todo;

namespace TaskManager.Application.Validators.Todo;

/// <summary>
///     وليديتور دريافت همه تسک های کاربر
/// </summary>
public class GetAllTodosByUserRequestValidator : AbstractValidator<GetAllTodosByUserRequest>
{
    public GetAllTodosByUserRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await userRepository.IsExist(id, cancellationToken))
            .WithMessage("کاربر مورد نظر یافت نشد");
    }
}