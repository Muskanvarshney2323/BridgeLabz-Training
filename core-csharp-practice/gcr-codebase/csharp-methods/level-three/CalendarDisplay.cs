using System;
class CalendarDisplay
{
    static string[] months = { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    static int[] daysInMonthCommon = { 0,31,28,31,30,31,30,31,31,30,31,30,31 };

    static void Main()
    {
        Console.Write("Enter month (1-12): "); int m = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter year: "); int y = Convert.ToInt32(Console.ReadLine());

        int days = DaysInMonth(m, y);
        int firstDay = FirstDayOfMonth(m, y); // 0=Sunday

        Console.WriteLine("\t" + months[m] + " " + y);
        Console.WriteLine("Su Mo Tu We Th Fr Sa");

        for (int i = 0; i < firstDay; i++) Console.Write("   ");
        for (int d = 1; d <= days; d++)
        {
            Console.Write($"{d,2} ");
            if ((firstDay + d) % 7 == 0) Console.WriteLine();
        }
        Console.WriteLine();
    }

    static bool IsLeap(int y) => (y % 4 == 0 && y % 100 != 0) || (y % 400 == 0);
    static int DaysInMonth(int m, int y) { if (m == 2) return IsLeap(y) ? 29 : 28; return daysInMonthCommon[m]; }

    // Gregorian algorithm to get first day of month
    static int FirstDayOfMonth(int m, int y)
    {
        int d = 1;
        int y0 = y - (14 - m) / 12;
        int x = y0 + y0/4 - y0/100 + y0/400;
        int m0 = m + 12 * ((14 - m) / 12) - 2;
        int d0 = (d + x + (31 * m0) / 12) % 7;
        return d0; // 0=Sunday
    }
}