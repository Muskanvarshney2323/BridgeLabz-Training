using System;
using System.Numerics;

class FactorialRecursion
{
    static void Main()
    {
        int n = ReadNonNegativeInt("Enter a non-negative integer to compute factorial: ");
        BigInteger result = Factorial(n);
        Console.WriteLine($"{n}! = {result}");
    }

    static int ReadNonNegativeInt(string prompt)
    {
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out int v) || v < 0)
        {
            Console.Write("Invalid input. Enter a non-negative integer: ");
        }
        return v;
    }

    static BigInteger Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }
}