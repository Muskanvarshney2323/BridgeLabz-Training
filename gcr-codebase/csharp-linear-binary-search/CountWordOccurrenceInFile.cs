using System;
using System.IO;
using System.Text.RegularExpressions;

// Count occurrences of a specific word in a file (case-insensitive)
public static class CountWordOccurrenceInFile
{
    public static int CountWord(string path, string word)
    {
        if (string.IsNullOrEmpty(word)) return 0;
        var pattern = $"\\b{Regex.Escape(word)}\\b";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);
        int count = 0;
        try
        {
            using (var sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    count += regex.Matches(line).Count;
                }
            }
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"I/O error: {ex.Message}");
        }
        return count;
    }

    public static void Main()
    {
        Console.WriteLine(CountWord("sample.txt", "the"));
    }
}