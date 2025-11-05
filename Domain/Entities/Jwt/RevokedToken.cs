namespace TaskManager.Domain.Entities.Jwt;

/// <summary>
///     توکن های ابطال شده
/// </summary>
public class RevokedToken
{
    /// <summary>
    ///     آی دی
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     مقدار کامل JWT Token
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    ///     تاریخ و ساعت لغو شدن توکن (UTC)
    /// </summary>
    public DateTime RevokedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     تاریخ و ساعت انقضای توکن (UTC)
    /// </summary>
    public DateTime ExpireAt { get; set; }
}