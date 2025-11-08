namespace TaskManager.Application.Responses.BaseResponses;

/// <summary>
///     پاسخ وضعیت
/// </summary>
/// <param name="hasError">ارور دارد / ندارد</param>
public class StatusResponse(bool hasError)
{
    /// <summary>
    ///     ارور دارد / ندارد
    /// </summary>
    public bool HasError { get; set; } = hasError;

    /// <summary>
    ///     پیام
    /// </summary>
    public string Message { get; set; } = hasError ? "خطا در عمليات" : "عمليات موفق";

    /// <summary>
    ///     ارور ها
    /// </summary>
    public List<string> Errors { get; set; }
}