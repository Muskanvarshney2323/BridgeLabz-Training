using System;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Problem 2: Buffered Streams - Efficient File Copy
/// Compares performance of BufferedStream vs normal FileStream.
/// Copies large file in chunks of 4 KB (4096 bytes).
/// </summary>
class BufferedStreamCopy
{
    const int BUFFER_SIZE = 4096; // 4 KB buffer

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Buffered Streams - Efficient File Copy          ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string sourceFile = "large_source.txt";
        string destBufferedFile = "dest_buffered.txt";
        string destUnbufferedFile = "dest_unbuffered.txt";

        try
        {
            // Create a large test file (10 MB)
            Console.WriteLine("Creating test file (10 MB)...");
            CreateLargeFile(sourceFile, 10 * 1024 * 1024); // 10 MB
            FileInfo sourceInfo = new FileInfo(sourceFile);
            Console.WriteLine($"✓ Created: {sourceFile} ({sourceInfo.Length / (1024 * 1024)} MB)\n");

            // Test 1: Copy with BufferedStream
            Console.WriteLine("=== Testing BUFFERED STREAM Copy ===");
            Stopwatch bufferedStopwatch = Stopwatch.StartNew();
            CopyFileWithBufferedStream(sourceFile, destBufferedFile);
            bufferedStopwatch.Stop();
            long bufferedTime = bufferedStopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Time taken: {bufferedTime} ms");
            Console.WriteLine($"✓ File copied to: {destBufferedFile}\n");

            // Test 2: Copy with unbuffered FileStream
            Console.WriteLine("=== Testing UNBUFFERED FileStream Copy ===");
            Stopwatch unbufferedStopwatch = Stopwatch.StartNew();
            CopyFileWithUnbufferedStream(sourceFile, destUnbufferedFile);
            unbufferedStopwatch.Stop();
            long unbufferedTime = unbufferedStopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Time taken: {unbufferedTime} ms");
            Console.WriteLine($"✓ File copied to: {destUnbufferedFile}\n");

            // Performance Comparison
            Console.WriteLine("=== PERFORMANCE COMPARISON ===");
            Console.WriteLine($"Buffered Stream Time:   {bufferedTime} ms");
            Console.WriteLine($"Unbuffered Stream Time: {unbufferedTime} ms");
            double improvement = ((double)(unbufferedTime - bufferedTime) / unbufferedTime) * 100;
            Console.WriteLine($"Improvement: {improvement:F2}%\n");

            if (bufferedTime < unbufferedTime)
            {
                Console.WriteLine($"✓ BufferedStream is faster by {unbufferedTime - bufferedTime} ms");
            }
            else
            {
                Console.WriteLine($"✓ UnbufferedStream is faster by {bufferedTime - unbufferedTime} ms");
            }

            // Verify files are identical
            if (FilesAreIdentical(destBufferedFile, destUnbufferedFile))
            {
                Console.WriteLine("✓ Both copied files are identical!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        finally
        {
            // Cleanup
            if (File.Exists(sourceFile)) File.Delete(sourceFile);
            if (File.Exists(destBufferedFile)) File.Delete(destBufferedFile);
            if (File.Exists(destUnbufferedFile)) File.Delete(destUnbufferedFile);
        }
    }

    static void CreateLargeFile(string filePath, int sizeInBytes)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            byte[] buffer = new byte[4096];
            Random random = new Random();
            int bytesWritten = 0;

            while (bytesWritten < sizeInBytes)
            {
                random.NextBytes(buffer);
                int toWrite = Math.Min(buffer.Length, sizeInBytes - bytesWritten);
                fs.Write(buffer, 0, toWrite);
                bytesWritten += toWrite;
            }
        }
    }

    static void CopyFileWithBufferedStream(string sourceFile, string destFile)
    {
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
        using (BufferedStream bufferedSource = new BufferedStream(sourceStream, BUFFER_SIZE))
        using (FileStream destStream = new FileStream(destFile, FileMode.Create, FileAccess.Write))
        using (BufferedStream bufferedDest = new BufferedStream(destStream, BUFFER_SIZE))
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            int bytesRead = 0;

            while ((bytesRead = bufferedSource.Read(buffer, 0, buffer.Length)) > 0)
            {
                bufferedDest.Write(buffer, 0, bytesRead);
            }
        }
    }

    static void CopyFileWithUnbufferedStream(string sourceFile, string destFile)
    {
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
        using (FileStream destStream = new FileStream(destFile, FileMode.Create, FileAccess.Write))
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            int bytesRead = 0;

            while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                destStream.Write(buffer, 0, bytesRead);
            }
        }
    }

    static bool FilesAreIdentical(string file1, string file2)
    {
        FileInfo info1 = new FileInfo(file1);
        FileInfo info2 = new FileInfo(file2);

        if (info1.Length != info2.Length)
            return false;

        using (FileStream fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
        using (FileStream fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer1 = new byte[4096];
            byte[] buffer2 = new byte[4096];
            int bytesRead1, bytesRead2;

            while (true)
            {
                bytesRead1 = fs1.Read(buffer1, 0, buffer1.Length);
                bytesRead2 = fs2.Read(buffer2, 0, buffer2.Length);

                if (bytesRead1 != bytesRead2)
                    return false;

                if (bytesRead1 == 0)
                    break;

                for (int i = 0; i < bytesRead1; i++)
                {
                    if (buffer1[i] != buffer2[i])
                        return false;
                }
            }
        }

        return true;
    }
}
