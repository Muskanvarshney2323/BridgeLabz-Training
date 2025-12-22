using System;
class Armstrong
{
    static void Main()
    {
        Console.Write("Enter an integer: ");
        int number = Convert.ToInt32(Console.ReadLine());
        int sum = 0;
        int originalNumber = number;

        while (originalNumber != 0)
        {
            int remainder = originalNumber % 10;
            sum += remainder * remainder * remainder;
            originalNumber /= 10;
        }

        if (sum == number)
        {
            Console.WriteLine($"{number} is an Armstrong number.");
        }
        else
        {
            Console.WriteLine($"{number} is not an Armstrong number.");
        }
    }
}