
using System;
class SpringSeason
{
    static void Main()
    {
        Console.Write("Enter the month (as an integer): ");
        int month = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the day: ");
        int day = Convert.ToInt32(Console.ReadLine());

        if ((month == 3 && day >= 20) || (month == 4) || (month == 5) || (month == 6 && day <= 20))
        {
            Console.WriteLine("It's a Spring Season");
        }
        else
        {
            Console.WriteLine("Not a Spring Season");
        }
    }
}