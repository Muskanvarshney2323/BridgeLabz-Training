using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the first number (dividend): ");
        int number1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the second number (divisor): ");
        int number2 = Convert.ToInt32(Console.ReadLine());

        int quotient = number1 / number2;
        int remainder = number1 % number2;

        Console.WriteLine("The Quotient is " + quotient + " and Remainder is " + remainder + " of two numbers " + number1 + " and " + number2);
    }
}