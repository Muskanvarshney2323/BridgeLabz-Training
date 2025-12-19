
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the first number (number1): ");
        int number1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the second number (number2): ");
        int number2 = Convert.ToInt32(Console.ReadLine());

        // Swapping the numbers
        int temp = number1;
        number1 = number2;
        number2 = temp;

        Console.WriteLine("The swapped numbers are " + number1 + " and " + number2);
    }
}