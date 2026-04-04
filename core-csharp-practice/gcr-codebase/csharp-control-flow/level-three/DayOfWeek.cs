using System;
class DayOfWeek
{
    static void Main()
    {
        Console.Write("Enter month (1-12): ");
        int m = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter day (1-31): ");
        int d = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter year (e.g., 2024): ");
        int y = Convert.ToInt32(Console.ReadLine());

        int y0 = y - (14 - m) / 12;
        int x = y0 + y0 / 4 - y0 / 100 + y0 / 400;
        int m0 = m + 12 * ((14 - m) / 12) - 2;
        int d0 = (d + x + (31 * m0) / 12) % 7;

        Console.WriteLine($"The day of the week is: {d0} (0=Sunday, 1=Monday, ..., 6=Saturday)");
    }
}