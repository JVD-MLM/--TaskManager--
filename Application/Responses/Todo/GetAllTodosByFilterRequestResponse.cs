using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دريافت همه تسک ها با فیلتر
/// </summary>
public class GetAllTodosByFilterRequestResponse
{
    /// <summary>
    /// لیست Dto تسک ها
    /// </summary>
    public List<TodoDto> Items { get; set; }
}