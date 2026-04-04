using System;
class Program   
{
    static void Main()
    {
        double total = 0.0;

        while (true)
        {
            Console.Write("Enter a number (0 or negative to stop): ");
            double number = Convert.ToDouble(Console.ReadLine());

            if (number <= 0)
            {
                break;
            }

            total += number;
        }

        Console.WriteLine($"The sum of the entered numbers is {total}");
    }
}