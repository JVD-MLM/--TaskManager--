using TaskManager.Application.DTOs.User;

namespace TaskManager.Application.Responses.User;

/// <summary>
///     پاسخ درخواست دريافت کاربر با کد ملی
/// </summary>
public class GetUserByNationalCodeRequestResponse
{
    /// <summary>
    ///     Dto کاربر
    /// </summary>
    public UserDto Item { get; set; }
}