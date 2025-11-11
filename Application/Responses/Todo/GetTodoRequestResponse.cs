using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دریافت تسک
/// </summary>
public class GetTodoRequestResponse
{
    public TodoDto Item { get; set; }
}