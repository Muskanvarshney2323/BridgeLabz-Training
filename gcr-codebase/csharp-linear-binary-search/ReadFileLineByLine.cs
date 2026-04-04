using System;
using System.IO;

// Read a file line by line using StreamReader
public static class ReadFileLineByLine
{
    public static void PrintLines(string path)
    {
        try
        {
            using (var sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"I/O error: {ex.Message}");
        }
    }

    public static void Main()
    {
        // Ensure a file "sample.txt" exists next to the executable or provide an absolute path
        PrintLines("sample.txt");
    }
}