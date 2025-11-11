using TaskManager.Application.DTOs.Todo;

namespace TaskManager.Application.Responses.Todo;

/// <summary>
///     پاسخ دريافت همه تسک ها با فیلتر
/// </summary>
public class GetAllTodosByFilterRequestResponse
{
    public List<TodoDto> Items { get; set; }
}