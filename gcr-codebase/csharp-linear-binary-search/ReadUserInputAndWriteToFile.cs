using System;
using System.IO;

// Read user input from console and append to a file using StreamWriter (paired with StreamReader concept)
public static class ReadUserInputAndWriteToFile
{
    public static void SaveInput(string path)
    {
        try
        {
            Console.WriteLine("Enter lines (blank line to finish):");
            using (var sw = new StreamWriter(path, append: true))
            {
                string line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    sw.WriteLine(line);
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
        SaveInput("user_output.txt");
    }
}