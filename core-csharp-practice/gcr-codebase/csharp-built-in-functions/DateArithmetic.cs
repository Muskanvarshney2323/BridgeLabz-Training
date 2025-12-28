using System;
class DateArithmetic
{
    static void Main()
    {
        Console.Write("Enter a date (e.g., 2025-12-28 or 28/12/2025): ");
        string input = Console.ReadLine() ?? string.Empty;
        if (!DateTime.TryParse(input, out DateTime dt))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        DateTime plus7Days = dt.AddDays(7);
        DateTime plus1Month = plus7Days.AddMonths(1);
        DateTime plus2Years = plus1Month.AddYears(2);

        // DateTime has no AddWeeks method in many .NET versions; subtract 3 weeks by subtracting 21 days
        DateTime result = plus2Years.AddDays(-21);

        Console.WriteLine($"Original date: {dt:yyyy-MM-dd}");
        Console.WriteLine($"After +7 days: {plus7Days:yyyy-MM-dd}");
        Console.WriteLine($"After +1 month: {plus1Month:yyyy-MM-dd}");
        Console.WriteLine($"After +2 years: {plus2Years:yyyy-MM-dd}");
        Console.WriteLine($"After -3 weeks: {result:yyyy-MM-dd}");
    }
}