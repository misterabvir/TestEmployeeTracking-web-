using System.Globalization;

namespace Infrastructure.Client.Extensions;

public static class DateExtension
{
    public static string GetDateString(this string date)
    {
        if(DateOnly.TryParseExact(date, "M/d/yyyy", out DateOnly result))
        {
            return $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(result.Month)} {result.Day}, {result.Year}";
        }
        return string.Empty;
    }
}
