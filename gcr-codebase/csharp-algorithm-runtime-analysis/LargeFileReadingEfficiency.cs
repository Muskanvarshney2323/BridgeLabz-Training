using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace BridgeLabz.AlgorithmRuntimeAnalysis
{
    // Compare StreamReader vs FileStream for reading files of different sizes
    public static class LargeFileReadingEfficiency
    {
        // Generates a file of ~sizeInBytes bytes filled with repeated lines of text. Use with caution for very large sizes.
        static void EnsureTestFile(string path, long sizeInBytes)
        {
            if (File.Exists(path) && new FileInfo(path).Length >= sizeInBytes) return;
            Console.WriteLine($"Generating test file: {path} (~{sizeInBytes} bytes). This may take time/disk.");
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var sw = new StreamWriter(fs, Encoding.UTF8))
            {
                string line = new string('A', 1024);
                long written = 0;
                while (written < sizeInBytes)
                {
                    sw.WriteLine(line);
                    written += line.Length + Environment.NewLine.Length;
                }
            }
        }

        static long ReadWithStreamReader(string path)
        {
            var sw = Stopwatch.StartNew();
            using (var sr = new StreamReader(path))
            {
                while (sr.ReadLine() != null) { }
            }
            sw.Stop(); return sw.ElapsedMilliseconds;
        }

        static long ReadWithFileStream(string path)
        {
            var sw = Stopwatch.StartNew();
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[81920];
                while (fs.Read(buffer, 0, buffer.Length) > 0) { }
            }
            sw.Stop(); return sw.ElapsedMilliseconds;
        }

        public static void Main()
        {
            Console.WriteLine("File reading efficiency: StreamReader vs FileStream (buffered)");
            string path = Path.Combine(Path.GetTempPath(), "big_test_file.txt");

            // For safety the example sizes are modest by default. Increase to 100MB/500MB only if you have disk/time available.
            var sizes = new (long size, string label)[] { (1L << 20, "1MB"), (100L << 20, "100MB") /*, (500L << 20, "500MB")*/ };

            foreach (var (size, label) in sizes)
            {
                EnsureTestFile(path, size);
                Console.WriteLine($"--- Reading {label} ---");
                Console.WriteLine($"StreamReader: {ReadWithStreamReader(path)} ms");
                Console.WriteLine($"FileStream:   {ReadWithFileStream(path)} ms");
                Console.WriteLine();
            }

            Console.WriteLine("Note: FileStream reading bytes with appropriate buffer often outperforms character-based StreamReader for large binary-like workloads.");
        }
    }
}
