using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دريافت همه تسک های کاربر
/// </summary>
public class GetAllTodosByUserRequestResponse
{
    public List<TodoDto> Items { get; set; }
}