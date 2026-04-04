using System;
class TimeZonesDateTimeOffset
{
    static void Main()
    {
        // Get current UTC time as DateTimeOffset
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;

        Console.WriteLine("Current times in selected time zones:");

        // GMT (UTC)
        Console.WriteLine($"GMT (UTC): {utcNow.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss")}");

        // IST (India Standard Time) - UTC+5:30
        try
        {
            TimeZoneInfo ist = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTimeOffset istTime = TimeZoneInfo.ConvertTime(utcNow, ist);
            Console.WriteLine($"IST : {istTime.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
        catch (TimeZoneNotFoundException)
        {
            // Fallback calculation if system id not found
            DateTimeOffset istTime = utcNow.ToOffset(TimeSpan.FromHours(5.5));
            Console.WriteLine($"IST : {istTime.ToString("yyyy-MM-dd HH:mm:ss")} (calculated offset)");
        }

        // PST (Pacific Standard Time) - typically "Pacific Standard Time" on Windows
        try
        {
            TimeZoneInfo pst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            DateTimeOffset pstTime = TimeZoneInfo.ConvertTime(utcNow, pst);
            Console.WriteLine($"PST : {pstTime.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
        catch (TimeZoneNotFoundException)
        {
            // Fallback to UTC-8
            DateTimeOffset pstTime = utcNow.ToOffset(TimeSpan.FromHours(-8));
            Console.WriteLine($"PST : {pstTime.ToString("yyyy-MM-dd HH:mm:ss")} (calculated offset)");
        }
    }
}