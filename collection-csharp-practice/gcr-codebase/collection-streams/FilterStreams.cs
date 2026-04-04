using System;
using System.IO;

/// <summary>
/// Problem 6: Filter Streams - Convert Uppercase to Lowercase
/// Reads a text file and writes its contents to another file,
/// converting all uppercase letters to lowercase.
/// </summary>
class FilterStreamsProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Filter Streams - Convert Uppercase to Lowercase ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string sourceFile = "source_case.txt";
        string destFile = "dest_lowercase.txt";

        try
        {
            // Create sample file with mixed case
            Console.WriteLine("Creating sample file with mixed case...");
            string sampleContent = @"This Is A TEST FILE.
It Contains UPPERCASE and lowercase LETTERS.
HELLO WORLD from C# STREAMS!
FileStream and StreamWriter ARE BEING USED.
The Program Converts ALL UPPERCASE to lowercase.";

            File.WriteAllText(sourceFile, sampleContent);
            Console.WriteLine($"✓ Created: {sourceFile}\n");

            // Display source content
            Console.WriteLine("=== SOURCE FILE CONTENT ===");
            Console.WriteLine(File.ReadAllText(sourceFile));

            // Read from source and write to destination with case conversion
            Console.WriteLine("\n=== Converting to Lowercase ===");
            ConvertToLowercase(sourceFile, destFile);
            Console.WriteLine($"✓ Content written to: {destFile}\n");

            // Display destination content
            Console.WriteLine("=== DESTINATION FILE CONTENT ===");
            Console.WriteLine(File.ReadAllText(destFile));
            
            Console.WriteLine("\n✓ Conversion completed successfully!");
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
            if (File.Exists(sourceFile)) File.Delete(sourceFile);
            if (File.Exists(destFile)) File.Delete(destFile);
        }
    }

    static void ConvertToLowercase(string sourceFile, string destFile)
    {
        try
        {
            using (StreamReader reader = new StreamReader(sourceFile))
            using (BufferedStream bufferedSource = new BufferedStream(reader.BaseStream, 4096))
            using (StreamWriter writer = new StreamWriter(destFile))
            using (BufferedStream bufferedDest = new BufferedStream(writer.BaseStream, 4096))
            {
                string line;
                int lineCount = 0;

                // Read line by line and convert to lowercase
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                while ((line = reader.ReadLine()) != null)
                {
                    string convertedLine = line.ToLower();
                    writer.WriteLine(convertedLine);
                    lineCount++;
                }

                Console.WriteLine($"Processed {lineCount} lines");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during conversion: {ex.Message}");
            throw;
        }
    }
}
