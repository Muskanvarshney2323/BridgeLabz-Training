using System;
class DateComparison
{
    static void Main()
    {
        Console.Write("Enter first date (e.g., 2025-12-28): ");
        string a = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter second date (e.g., 2026-01-01): ");
        string b = Console.ReadLine() ?? string.Empty;

        if (!DateTime.TryParse(a, out DateTime d1) || !DateTime.TryParse(b, out DateTime d2))
        {
            Console.WriteLine("One or both dates were in an invalid format.");
            return;
        }

        int cmp = DateTime.Compare(d1.Date, d2.Date);
        if (cmp < 0) Console.WriteLine($"{d1:yyyy-MM-dd} is before {d2:yyyy-MM-dd}");
        else if (cmp > 0) Console.WriteLine($"{d1:yyyy-MM-dd} is after {d2:yyyy-MM-dd}");
        else Console.WriteLine("The two dates are the same.");
    }
}