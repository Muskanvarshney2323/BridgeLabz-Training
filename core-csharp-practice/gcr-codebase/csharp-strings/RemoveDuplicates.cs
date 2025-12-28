using System;
using System.Collections.Generic;
class RemoveDuplicates
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine() ?? string.Empty;
        string result = RemoveDuplicatesMethod(input);
        Console.WriteLine("Modified string: " + result);
    }

    public static string RemoveDuplicatesMethod(string s)
    {
        HashSet<char> seen = new HashSet<char>();
        var sb = new System.Text.StringBuilder();
        foreach (char c in s)
        {
            if (!seen.Contains(c))
            {
                seen.Add(c);
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}