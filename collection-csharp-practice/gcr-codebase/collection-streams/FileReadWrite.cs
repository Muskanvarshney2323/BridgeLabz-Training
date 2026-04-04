using System;
using System.IO;

/// <summary>
/// Problem 1: File Handling - Read and Write a Text File
/// Reads contents of a text file and writes it into a new file using FileStream.
/// Handles IOException if source file does not exist.
/// </summary>
class FileReadWrite
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║    File Handling - Read and Write a Text File     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string sourceFile = "source.txt";
        string destinationFile = "destination.txt";

        try
        {
            // Create sample source file if it doesn't exist
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine($"Creating sample source file: {sourceFile}");
                File.WriteAllText(sourceFile, "Hello World!\nThis is a test file.\nFile handling in C#.\n");
            }

            Console.WriteLine($"✓ Source file found: {sourceFile}\n");

            // Read from source file using FileStream
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                Console.WriteLine("=== Reading from Source File ===");
                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string content = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(content);
                }
                Console.WriteLine();
            }

            // Write to destination file using FileStream
            Console.WriteLine($"=== Writing to Destination File: {destinationFile} ===");
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destinationStream.Write(buffer, 0, bytesRead);
                }
            }

            Console.WriteLine($"✓ File copied successfully to: {destinationFile}\n");

            // Verify destination file
            Console.WriteLine("=== Verifying Destination File ===");
            string destinationContent = File.ReadAllText(destinationFile);
            Console.WriteLine(destinationContent);
            Console.WriteLine("✓ File copy verified successfully!");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"✗ Error: Source file not found - {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ IO Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Unexpected Error: {ex.Message}");
        }
    }
}
