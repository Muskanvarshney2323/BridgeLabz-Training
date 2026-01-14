using System;
using System.Diagnostics;
using System.Text;

namespace BridgeLabz.AlgorithmRuntimeAnalysis
{
    // Compare string concatenation vs StringBuilder for concatenating many small strings
    public static class StringConcatenationPerformance
    {
        static string BuildWithString(int n)
        {
            string s = string.Empty;
            for (int i = 0; i < n; i++) s += i.ToString();
            return s;
        }

        static string BuildWithStringBuilder(int n)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < n; i++) sb.Append(i);
            return sb.ToString();
        }

        static void Bench(int n)
        {
            Console.WriteLine($"--- Concatenation N={n} ---");
            var sw = new Stopwatch();

            try
            {
                sw.Restart();
                var a = BuildWithString(n);
                sw.Stop(); Console.WriteLine($"string concatenation: {sw.Elapsed.TotalMilliseconds:F4} ms");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("string concatenation: Unusable (OOM)");
            }

            sw.Restart();
            var b = BuildWithStringBuilder(n);
            sw.Stop(); Console.WriteLine($"StringBuilder: {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine();
        }

        public static void Main()
        {
            Console.WriteLine("String concatenation performance: string vs StringBuilder");
            Bench(1_000);
            Bench(10_000);
            Bench(1_000_000);
            Console.WriteLine("Expected: StringBuilder is far more efficient for large N.");
        }
    }
}
