using System;
using System.Diagnostics;
using System.Text;

// Compare performance of StringBuilder vs string concatenation
public static class CompareStringBuilderPerformance
{
    public static void Compare(int iterations)
    {
        var sw = Stopwatch.StartNew();
        string s = string.Empty;
        for (int i = 0; i < iterations; i++) s += "x";
        sw.Stop();
        Console.WriteLine($"Concatenation time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        var sb = new StringBuilder(iterations);
        for (int i = 0; i < iterations; i++) sb.Append("x");
        var r = sb.ToString();
        sw.Stop();
        Console.WriteLine($"StringBuilder time: {sw.ElapsedMilliseconds} ms");
    }

    // Sample usage
    public static void Main()
    {
        Compare(100_000); // typically shows StringBuilder is faster
    }
}