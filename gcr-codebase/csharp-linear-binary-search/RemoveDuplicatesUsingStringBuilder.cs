using System;
using System.Text;

// Remove duplicate characters while preserving order using StringBuilder
public static class RemoveDuplicatesUsingStringBuilder
{
    public static string RemoveDuplicates(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var seen = new bool[256];
        var sb = new StringBuilder();
        foreach (char c in input)
        {
            if (!seen[c])
            {
                sb.Append(c);
                seen[c] = true;
            }
        }
        return sb.ToString();
    }

    // Sample usage
    public static void Main()
    {
        var s = "programming";
        Console.WriteLine(RemoveDuplicates(s)); // prints "progamin"
    }
}