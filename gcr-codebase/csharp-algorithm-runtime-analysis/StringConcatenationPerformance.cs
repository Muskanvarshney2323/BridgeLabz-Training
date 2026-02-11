using System;
using System.Diagnostics;
using System.Text;

class StringAppendPerformanceTest
{
    private static readonly object syncLock = new object();

    static void Main(string[] args)
    {
        int[] counts = { 1000, 10000, 1000000 };

        Console.WriteLine("Count\tString(ms)\tStringBuilder(ms)\tThreadSafeBuilder(ms)");

        foreach (int count in counts)
        {
            long normalStr = MeasureStringConcat(count);
            long fastBuilder = MeasureBuilderConcat(count);
            long safeBuilder = MeasureThreadSafeConcat(count);

            Console.WriteLine($"{count}\t{normalStr}\t\t{fastBuilder}\t\t\t{safeBuilder}");
        }
    }

    // Immutable String concatenation (Quadratic time)
    static long MeasureStringConcat(int times)
    {
        Stopwatch watch = Stopwatch.StartNew();
        string text = string.Empty;

        for (int i = 0; i < times; i++)
        {
            text = text + "x";
        }

        watch.Stop();
        return watch.ElapsedMilliseconds;
    }

    // Mutable StringBuilder (Linear time)
    static long MeasureBuilderConcat(int times)
    {
        Stopwatch watch = Stopwatch.StartNew();
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < times; i++)
        {
            builder.Append("x");
        }

        watch.Stop();
        return watch.ElapsedMilliseconds;
    }

    // Thread-safe simulation using lock
    static long MeasureThreadSafeConcat(int times)
    {
        Stopwatch watch = Stopwatch.StartNew();
        StringBuilder sharedBuilder = new StringBuilder();

        for (int i = 0; i < times; i++)
        {
            lock (syncLock)
            {
                sharedBuilder.Append("x");
            }
        }

        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}
