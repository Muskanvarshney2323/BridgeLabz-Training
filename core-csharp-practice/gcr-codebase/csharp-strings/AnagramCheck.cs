using System;
using System.Collections.Generic;
class AnagramCheck
{
    static void Main()
    {
        Console.Write("Enter first string: ");
        string s1 = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter second string: ");
        string s2 = Console.ReadLine() ?? string.Empty;
        bool anagram = AreAnagramsMethod(s1, s2);
        Console.WriteLine(anagram ? "The strings are anagrams." : "The strings are NOT anagrams.");
    }

    public static bool AreAnagramsMethod(string a, string b)
    {
        string s1 = a.Replace(" ", "").ToLower();
        string s2 = b.Replace(" ", "").ToLower();
        if (s1.Length != s2.Length) return false;
        var freq = new Dictionary<char, int>();
        foreach (char c in s1)
        {
            if (!freq.ContainsKey(c)) freq[c] = 0;
            freq[c]++;
        }
        foreach (char c in s2)
        {
            if (!freq.ContainsKey(c)) return false;
            freq[c]--;
            if (freq[c] < 0) return false;
        }
        return true;
    }
}