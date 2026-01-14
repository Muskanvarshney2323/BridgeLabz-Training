using System;
using System.Diagnostics;

namespace BridgeLabz.AlgorithmRuntimeAnalysis
{
    // Compare naive recursive Fibonacci vs iterative approach
    public static class FibonacciRecursiveVsIterative
    {
        public static long FibonacciRecursive(int n)
        {
            if (n <= 1) return n;
            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }

        public static long FibonacciIterative(int n)
        {
            if (n <= 1) return n;
            long a = 0, b = 1;
            for (int i = 2; i <= n; i++) { long sum = a + b; a = b; b = sum; }
            return b;
        }

        static void Bench(int n)
        {
            Console.WriteLine($"--- Fibonacci N={n} ---");
            var sw = new Stopwatch();

            if (n <= 40) // recursive grows exponentially — keep it safe
            {
                sw.Restart();
                var r = FibonacciRecursive(n);
                sw.Stop(); Console.WriteLine($"Recursive: {r} in {sw.Elapsed.TotalMilliseconds:F4} ms");
            }
            else
            {
                Console.WriteLine("Recursive: Unfeasible (exponential time) — skipped");
            }

            sw.Restart();
            var it = FibonacciIterative(n);
            sw.Stop(); Console.WriteLine($"Iterative: {it} in {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine();
        }

        public static void Main()
        {
            Console.WriteLine("Fibonacci: Recursive vs Iterative");
            Bench(10);
            Bench(30);
            Bench(50);
            Console.WriteLine("Expected: recursive is infeasible for large N; iterative is linear and fast.");
        }
    }
}
