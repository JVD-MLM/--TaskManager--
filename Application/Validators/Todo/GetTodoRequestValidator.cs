using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.Todo;

namespace TaskManager.Application.Validators.Todo;

/// <summary>
///     وليديتور دریافت تسک
/// </summary>
public class GetTodoRequestValidator : AbstractValidator<GetTodoRequest>
{
    public GetTodoRequestValidator(ITodoRepository todoRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await todoRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");
    }
}