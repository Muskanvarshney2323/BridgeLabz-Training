using System;
class CountTheNo
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int count = 0;

        while (number != 0)
        {
            number /= 10; // Remove the last digit
            count++;      // Increment the count
        }

        Console.WriteLine($"Number of digits: {count}");
    }
}