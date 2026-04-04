    
using System;
class SumOfAllNo
{
    static void Main()
    {
        double[] numbers = new double[10];
        double total = 0.0;
        int index = 0;

        while (true)
        {
            Console.WriteLine("Enter a positive number (or enter 0 or a negative number to stop): ");
            double input = Convert.ToDouble(Console.ReadLine());

            if (input <= 0 || index >= 10)
            {
                break;
            }

            numbers[index] = input;
            index++;
        }

        Console.WriteLine("You entered the following numbers:");
        for (int i = 0; i < index; i++)
        {
            Console.WriteLine(numbers[i]);
            total += numbers[i];
        }

        Console.WriteLine("The sum of all entered numbers is: " + total);
    }
}