using TaskManager.Application.DTOs.User;

namespace TaskManager.Application.Responses.User;

/// <summary>
///     پاسخ دریافت کاربر با آی دی
/// </summary>
public class GetUserByIdRequestResponse
{
    /// <summary>
    ///     Dto کاربر
    /// </summary>
    public UserDto Item { get; set; }
}