using System;
using System.IO;
using System.Text;

// Convert a byte stream to a character stream using StreamReader with explicit encoding
public static class ConvertByteStreamToCharStream
{
    public static void PrintAsChars(string path, Encoding encoding)
    {
        try
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs, encoding))
            {
                int ch;
                while ((ch = sr.Read()) != -1)
                {
                    Console.Write((char)ch);
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
        // Example reading with UTF8
        PrintAsChars("sample.bin", Encoding.UTF8);
    }
}