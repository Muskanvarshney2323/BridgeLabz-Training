using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Problem 9: Read a Large File Line by Line
/// Efficiently reads a large text file line by line
/// and prints only lines containing the word "error" (case insensitive).
/// </summary>
class LargeFileProcessingProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║     Read a Large File Line by Line                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string testFile = "large_log_file.txt";

        try
        {
            // Create a sample large log file
            Console.WriteLine("Creating sample large log file...");
            CreateSampleLogFile(testFile);
            FileInfo fileInfo = new FileInfo(testFile);
            Console.WriteLine($"✓ Created: {testFile} ({fileInfo.Length / 1024} KB)\n");

            // Search for lines containing "error"
            Console.WriteLine("=== Searching for lines containing 'error' ===\n");
            FindErrorLines(testFile);

            Console.WriteLine("\n✓ Search completed successfully!");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"✗ File not found: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ IO Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testFile))
                File.Delete(testFile);
        }
    }

    static void CreateSampleLogFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            string[] logEntries = new[]
            {
                "[2024-01-21 10:00:00] INFO: Application started",
                "[2024-01-21 10:00:05] DEBUG: Loading configuration",
                "[2024-01-21 10:00:10] INFO: Database connection established",
                "[2024-01-21 10:00:15] ERROR: Connection timeout occurred",
                "[2024-01-21 10:00:20] INFO: Retrying connection",
                "[2024-01-21 10:00:25] WARNING: Slow query detected",
                "[2024-01-21 10:00:30] ERROR: Authentication failed for user admin",
                "[2024-01-21 10:00:35] INFO: User login successful",
                "[2024-01-21 10:00:40] DEBUG: Processing request #1234",
                "[2024-01-21 10:00:45] ERROR: File not found: /data/config.xml",
                "[2024-01-21 10:00:50] INFO: Cache cleared",
                "[2024-01-21 10:00:55] WARNING: Memory usage at 85%",
                "[2024-01-21 10:01:00] ERROR: Network Error: Unable to reach server",
                "[2024-01-21 10:01:05] INFO: Fallback mode activated",
                "[2024-01-21 10:01:10] ERROR: Critical error in module A"
            };

            // Write log entries multiple times to create a large file
            for (int i = 0; i < 1000; i++)
            {
                foreach (var entry in logEntries)
                {
                    writer.WriteLine(entry);
                }
            }
        }
    }

    static void FindErrorLines(string filePath)
    {
        int totalLines = 0;
        int errorCount = 0;
        List<string> errorLines = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            
            while ((line = reader.ReadLine()) != null)
            {
                totalLines++;

                // Case-insensitive search for "error"
                if (line.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    errorCount++;
                    errorLines.Add(line);

                    // Display first 10 error lines
                    if (errorCount <= 10)
                    {
                        Console.WriteLine($"Line {totalLines}: {line}");
                    }
                }
            }
        }

        // Display summary
        Console.WriteLine($"\n--- Summary ---");
        Console.WriteLine($"Total lines read: {totalLines}");
        Console.WriteLine($"Lines containing 'error': {errorCount}");
        Console.WriteLine($"(Showing first 10 of {errorCount} error lines)");
    }
}
