using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;

namespace TaskManager.Application.Validators.Todo;

/// <summary>
///     وليديتور حذف تسک
/// </summary>
public class DeleteTodoRequestValidator : AbstractValidator<DeleteTodoRequest>
{
    public DeleteTodoRequestValidator(ITodoRepository todoRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await todoRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");
    }
}