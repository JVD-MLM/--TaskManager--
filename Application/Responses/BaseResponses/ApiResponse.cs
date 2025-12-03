namespace TaskManager.Application.Responses.BaseResponses;

/// <summary>
///     پاسخ Api
/// </summary>
/// <typeparam name="T">نوع پاسخ Api</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    ///     پاسخ وضعیت
    /// </summary>
    public StatusResponse Status { get; set; }

    /// <summary>
    ///     دیتای Api
    /// </summary>
    public T Result { get; set; }
}