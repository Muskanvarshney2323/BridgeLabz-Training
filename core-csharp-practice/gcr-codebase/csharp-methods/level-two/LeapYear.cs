using System;
class LeapYear
{
    static void Main()
    {
        Console.Write("Enter a year (>= 1582): ");
        int y = Convert.ToInt32(Console.ReadLine());
        if (y < 1582) { Console.WriteLine("Year must be >= 1582."); return; }
        Console.WriteLine(IsLeapYear(y) ? "Leap Year" : "Not a Leap Year");
    }

    public static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}