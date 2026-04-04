using System;
using System.Collections.Generic;
class MostFrequentChar
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string s = Console.ReadLine() ?? string.Empty;
        char? ch = MostFrequentCharMethod(s);
        if (ch.HasValue) Console.WriteLine("Most Frequent Character: '" + ch.Value + "'");
        else Console.WriteLine("Input string is empty.");
    }

    public static char? MostFrequentCharMethod(string s)
    {
        if (string.IsNullOrEmpty(s)) return null;
        var freq = new Dictionary<char, int>();
        foreach (char c in s)
        {
            if (char.IsWhiteSpace(c)) continue;
            if (!freq.ContainsKey(c)) freq[c] = 0;
            freq[c]++;
        }
        int max = 0; char result = '\0';
        foreach (var kv in freq)
        {
            if (kv.Value > max) { max = kv.Value; result = kv.Key; }
        }
        return result;
    }
}