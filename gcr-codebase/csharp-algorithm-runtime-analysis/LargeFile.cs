using System;
using System.Diagnostics;
using System.IO;

class FileReadPerformanceTest
{
    static void Main(string[] args)
    {
        string path = "largefile.txt"; // assume file already exists

        Console.WriteLine("Size\tStreamReader(ms)\tFileStream(ms)");

        EvaluateRead(path, "1MB");
        EvaluateRead(path, "100MB");
        EvaluateRead(path, "500MB");
    }

    static void EvaluateRead(string filePath, string label)
    {
        long textReadTime = MeasureStreamReader(filePath);
        long byteReadTime = MeasureFileStream(filePath);

        Console.WriteLine($"{label}\t{textReadTime}\t\t\t{byteReadTime}");
    }

    // Text-based reading (character level)
    static long MeasureStreamReader(string filePath)
    {
        Stopwatch timer = Stopwatch.StartNew();

        using (StreamReader sr = new StreamReader(filePath))
        {
            while (sr.Read() != -1)
            {
                // reading character by character
            }
        }

        timer.Stop();
        return timer.ElapsedMilliseconds;
    }

    // Byte-based reading (buffered, faster)
    static long MeasureFileStream(string filePath)
    {
        Stopwatch timer = Stopwatch.StartNew();
        byte[] chunk = new byte[8192];

        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            while (stream.Read(chunk, 0, chunk.Length) > 0)
            {
                // reading raw bytes
            }
        }

        timer.Stop();
        return timer.ElapsedMilliseconds;
    }
}
