using System;
class DateFormatting
{
    static void Main()
    {
        DateTime now = DateTime.Now;
        Console.WriteLine("Current date in different formats:");
        Console.WriteLine($"dd/MM/yyyy : {now.ToString("dd/MM/yyyy")}");
        Console.WriteLine($"yyyy-MM-dd : {now.ToString("yyyy-MM-dd")}");
        Console.WriteLine($"EEE, MMM dd, yyyy : {now.ToString("ddd, MMM dd, yyyy")}");
    }
}