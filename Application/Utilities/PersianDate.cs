using System.Globalization;

namespace TaskManager.Application.Utilities;

/// <summary>
///     تبدیل تاریخ میلادی به شمسی
/// </summary>
public static class PersianDate
{
    public static string ToPersianDateTime(this DateTime date) // برای DateTime
    {
        var iran = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var local = TimeZoneInfo.ConvertTimeFromUtc(date, iran);

        var pc = new PersianCalendar();

        var year = pc.GetYear(local);
        var month = pc.GetMonth(local);
        var day = pc.GetDayOfMonth(local);

        var hour = pc.GetHour(local);
        var minute = pc.GetMinute(local);
        var second = pc.GetSecond(local);

        return $"{year:0000}/{month:00}/{day:00} {hour:00}:{minute:00}:{second:00}";
    }

    public static string ToPersianDateTime(this DateTime? date) // برای DateTime?
    {
        return date.HasValue ? date.Value.ToPersianDateTime() : null;
    }
}