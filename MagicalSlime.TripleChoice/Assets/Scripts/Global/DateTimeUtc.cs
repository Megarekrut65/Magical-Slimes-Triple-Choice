using System;
using System.Globalization;

namespace Global
{
    public static class DateTimeUtc
    {
        public static DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
        public static string NowInvariant => Now.ToString(CultureInfo.InvariantCulture);
    }
}