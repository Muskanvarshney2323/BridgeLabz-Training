using System;
using System.Text;

// Efficiently concatenate an array of strings using StringBuilder
public static class ConcatenateStringsUsingStringBuilder
{
    public static string Concatenate(string[] parts)
    {
        if (parts == null || parts.Length == 0) return string.Empty;
        // If approximate size known, preinitialize capacity
        var total = 0;
        foreach (var p in parts) total += (p?.Length ?? 0);
        var sb = new StringBuilder(total);
        foreach (var p in parts)
        {
            if (!string.IsNullOrEmpty(p)) sb.Append(p);
        }
        return sb.ToString();
    }

    // Sample usage
    public static void Main()
    {
        var arr = new[] { "Hello", ", ", "World", "!" };
        Console.WriteLine(Concatenate(arr)); // prints "Hello, World!"
    }
}