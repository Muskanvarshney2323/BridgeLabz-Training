using System;
using System.Diagnostics;

class FibonacciPerformanceTest
{
    static void Main(string[] args)
    {
        int[] inputs = { 10, 30, 50 };

        Console.WriteLine("Value\tRecursive(ms)\tIterative(ms)");

        foreach (var value in inputs)
        {
            long recTime = CalculateRecursiveTime(value);
            long itrTime = CalculateIterativeTime(value);

            Console.WriteLine($"{value}\t{recTime}\t\t{itrTime}");
        }
    }

    // Recursive approach (Exponential Time Complexity)
    static int GetFibonacciRecursively(int num)
    {
        if (num < 2)
            return num;

        return GetFibonacciRecursively(num - 1) + GetFibonacciRecursively(num - 2);
    }

    // Iterative approach (Linear Time Complexity)
    static int GetFibonacciIteratively(int num)
    {
        if (num < 2)
            return num;

        int first = 0;
        int second = 1;

        for (int i = 2; i <= num; i++)
        {
            int next = first + second;
            first = second;
            second = next;
        }

        return second;
    }

    static long CalculateRecursiveTime(int num)
    {
        if (num > 40)
            return -1; // avoiding heavy computation

        Stopwatch timer = Stopwatch.StartNew();
        GetFibonacciRecursively(num);
        timer.Stop();

        return timer.ElapsedMilliseconds;
    }

    static long CalculateIterativeTime(int num)
    {
        Stopwatch timer = Stopwatch.StartNew();
        GetFibonacciIteratively(num);
        timer.Stop();

        return timer.ElapsedMilliseconds;
    }
}
