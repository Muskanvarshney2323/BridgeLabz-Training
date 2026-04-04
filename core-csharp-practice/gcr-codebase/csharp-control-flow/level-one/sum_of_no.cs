using System;
class Program
{
    static void Main()
    {
        double total = 0.0;
        double number;

        Console.Write("Enter a number (0 to stop): ");
        number = Convert.ToDouble(Console.ReadLine());

        while (number != 0)
        {
            total += number;
            Console.Write("Enter a number (0 to stop): ");
            number = Convert.ToDouble(Console.ReadLine());
        }

        Console.WriteLine($"The sum of the entered numbers is {total}");
    }
}