namespace TaskManager.Application.Responses;

/// <summary>
///     پاسخ وضعیت
/// </summary>
public class StatusResponse
{
    /// <summary>
    ///     ارور
    /// </summary>
    public bool HasError { get; set; } = false;

    /// <summary>
    ///     پیام
    /// </summary>
    public string Message { get; set; }
}