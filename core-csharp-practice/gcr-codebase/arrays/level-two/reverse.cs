using System;
class Reverse
{
    static void Main()
    {
        Console.WriteLine("Enter a number to reverse: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int maxDigit = 10;
        int[] digits = new int[maxDigit];
        int index = 0;

        while (number != 0)
        {
            if (index == maxDigit)
            {
                break;
            }
            digits[index] = number % 10;
            number /= 10;
            index++;
        }

        Console.WriteLine("Reversed number:");
        for (int i = index - 1; i >= 0; i--)
        {
            Console.Write(digits[i]);
        }
        Console.WriteLine();
    }
}