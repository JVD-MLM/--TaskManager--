using System.Globalization;

namespace TaskManager.Application.Utilities;

/// <summary>
///     تبدیل تاریخ میلادی به شمسی
/// </summary>
public static class PersianDate
{
    public static string ToPersianDateTime(this DateTime date)
    {
        var pc = new PersianCalendar();

        var year = pc.GetYear(date);
        var month = pc.GetMonth(date);
        var day = pc.GetDayOfMonth(date);

        var hour = pc.GetHour(date);
        var minute = pc.GetMinute(date);
        var second = pc.GetSecond(date);

        return $"{year:0000}/{month:00}/{day:00} {hour:00}:{minute:00}:{second:00}";
    }
}