using System;
class SubstringOccurrences
{
    static void Main()
    {
        Console.Write("Enter the string: ");
        string s = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter the substring to search: ");
        string sub = Console.ReadLine() ?? string.Empty;
        int count = CountSubstringOccurrencesMethod(s, sub);
        Console.WriteLine($"Occurrences: {count}");
    }

    public static int CountSubstringOccurrencesMethod(string s, string sub)
    {
        if (string.IsNullOrEmpty(sub)) return 0;
        int count = 0;
        for (int i = 0; i <= s.Length - sub.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < sub.Length; j++)
            {
                if (s[i + j] != sub[j]) { match = false; break; }
            }
            if (match) count++;
        }
        return count;
    }
}