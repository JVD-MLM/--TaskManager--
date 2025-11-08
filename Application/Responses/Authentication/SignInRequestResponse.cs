namespace TaskManager.Application.Responses.Authentication;

/// <summary>
///     پاسخ ورود کاربر
/// </summary>
public class SignInRequestResponse
{
    /// <summary>
    ///     توکن
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    ///     نام کاربری
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     نام
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     نام خانوادگی
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     نقش کاربر
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    ///     تاریخ انقضای توکن
    /// </summary>
    public string ExpiredAt { get; set; }
}