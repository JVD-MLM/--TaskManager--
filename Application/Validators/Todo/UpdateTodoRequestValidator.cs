using FluentValidation;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Todo;

namespace TaskManager.Application.Validators.Todo;

/// <summary>
///     وليديتور ویرایش تسک
/// </summary>
public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
{
    public UpdateTodoRequestValidator(ITodoRepository todoRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => await todoRepository.IsExist(id, cancellationToken))
            .WithMessage("رکورد مورد نظر یافت نشد");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("عنوان نمی‌تواند خالی باشد");
    }
}