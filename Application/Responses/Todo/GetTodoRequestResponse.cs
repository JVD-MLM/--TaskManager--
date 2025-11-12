using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دریافت تسک
/// </summary>
public class GetTodoRequestResponse
{
    /// <summary>
    /// Dto پروژه
    /// </summary>
    public TodoDto Item { get; set; }
}