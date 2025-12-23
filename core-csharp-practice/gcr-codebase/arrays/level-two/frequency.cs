using System;
class Frequency
{
    static void Main()
    {
        Console.WriteLine("Enter a number: ");
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

        int[] frequency = new int[10];

        for (int i = 0; i < index; i++)
        {
            frequency[digits[i]]++;
        }

        Console.WriteLine("Frequency of each digit:");
        for (int i = 0; i < 10; i++)
        {
            if (frequency[i] > 0)
            {
                Console.WriteLine("Digit {0} occurs {1} time(s)", i, frequency[i]);
            }
        }
    }
}