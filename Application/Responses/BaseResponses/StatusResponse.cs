namespace TaskManager.Application.Responses.BaseResponses;

/// <summary>
///     پاسخ وضعیت
/// </summary>
public class StatusResponse(bool hasError)
{
    /// <summary>
    ///     ارور
    /// </summary>
    public bool HasError { get; set; } = hasError;

    /// <summary>
    ///     پیام
    /// </summary>
    public string Message { get; set; } = hasError ? "خطا در عمليات" : "عمليات موفق";
}