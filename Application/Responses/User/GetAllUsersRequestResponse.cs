using TaskManager.Application.DTOs.User;

namespace TaskManager.Application.Responses.User;

/// <summary>
///     پاسخ دريافت همه کاربر ها
/// </summary>
public class GetAllUsersRequestResponse
{
    /// <summary>
    ///     لیست Dto کاربر ها
    /// </summary>
    public List<UserDto> Items { get; set; }
}