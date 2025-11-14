using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دريافت همه تسک ها بر اساس پروژه
/// </summary>
public class GetAllTodosByProjectRequestResponse
{
    /// <summary>
    ///     لیست Dto تسک ها
    /// </summary>
    public List<TodoDto> Items { get; set; }
}