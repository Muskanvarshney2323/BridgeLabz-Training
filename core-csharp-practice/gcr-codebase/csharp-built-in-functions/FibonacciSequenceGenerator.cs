using System;
using System.Collections.Generic;

class FibonacciSequenceGenerator
{
    static void Main()
    {
        int n = ReadInt("Enter number of terms to generate (non-negative): ");
        var seq = GenerateFibonacci(n);
        Console.WriteLine("Fibonacci sequence:");
        Console.WriteLine(string.Join(" ", seq));
    }

    static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out int value) || value < 0)
        {
            Console.Write("Invalid input. Enter a non-negative integer: ");
        }
        return value;
    }

    static List<long> GenerateFibonacci(int n)
    {
        var res = new List<long>();
        if (n == 0) return res;
        res.Add(0);
        if (n == 1) return res;
        res.Add(1);
        for (int i = 2; i < n; i++)
        {
            checked
            {
                res.Add(res[i - 1] + res[i - 2]);
            }
        }
        return res;
    }
}