using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دريافت همه تسک های کاربر
/// </summary>
public class GetAllTodosByUserRequestResponse
{
    /// <summary>
    ///     لیست Dto تسک ها
    /// </summary>
    public List<TodoDto> Items { get; set; }
}