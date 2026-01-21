using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Problem 8: Piped Streams - Inter-Thread Communication
/// Implements a program where one thread writes data into a PipeStream
/// and another thread reads data from it with proper synchronization.
/// </summary>
class PipedStreamsProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Piped Streams - Inter-Thread Communication      ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Create named pipe for communication
            Console.WriteLine("=== Starting Pipe-based Inter-Thread Communication ===\n");

            // Use file-based pipes for cross-platform compatibility
            string pipePath = "data_pipe.tmp";

            // Create writer and reader tasks
            Task writerTask = Task.Run(() => WriterThread(pipePath));
            
            // Small delay to ensure writer starts first
            System.Threading.Thread.Sleep(500);
            
            Task readerTask = Task.Run(() => ReaderThread(pipePath));

            // Wait for both tasks to complete
            Task.WaitAll(writerTask, readerTask);

            Console.WriteLine("\n✓ Pipe communication completed successfully!");

            // Cleanup
            if (File.Exists(pipePath))
                File.Delete(pipePath);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ IO Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static void WriterThread(string pipePath)
    {
        try
        {
            Console.WriteLine("[WRITER THREAD] Starting...");
            
            using (FileStream fs = new FileStream(pipePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                string[] messages = new[]
                {
                    "Hello from Writer Thread!",
                    "This is message 1",
                    "This is message 2",
                    "This is message 3",
                    "This is the final message",
                    "END"
                };

                foreach (var message in messages)
                {
                    writer.WriteLine(message);
                    writer.Flush();
                    Console.WriteLine($"[WRITER THREAD] Sent: {message}");
                    System.Threading.Thread.Sleep(500);
                }
            }

            Console.WriteLine("[WRITER THREAD] Finished writing data.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WRITER THREAD] Error: {ex.Message}");
        }
    }

    static void ReaderThread(string pipePath)
    {
        try
        {
            Console.WriteLine("[READER THREAD] Starting...");
            
            // Wait a bit for writer to create the file
            System.Threading.Thread.Sleep(200);

            using (FileStream fs = new FileStream(pipePath, FileMode.Open, FileAccess.Read, FileShare.Write))
            using (StreamReader reader = new StreamReader(fs))
            {
                string line;
                int messageCount = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    messageCount++;
                    Console.WriteLine($"[READER THREAD] Received: {line}");

                    if (line == "END")
                        break;

                    System.Threading.Thread.Sleep(300);
                }

                Console.WriteLine($"[READER THREAD] Total messages received: {messageCount}");
            }

            Console.WriteLine("[READER THREAD] Finished reading data.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[READER THREAD] Error: {ex.Message}");
        }
    }
}
