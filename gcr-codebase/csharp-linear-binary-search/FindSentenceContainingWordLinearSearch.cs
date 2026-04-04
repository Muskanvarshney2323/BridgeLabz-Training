using System;

// Linear search to find the first sentence containing a specific word
public static class FindSentenceContainingWordLinearSearch
{
    public static int IndexOfFirstSentenceWithWord(string[] sentences, string word)
    {
        if (sentences == null || string.IsNullOrEmpty(word)) return -1;
        for (int i = 0; i < sentences.Length; i++)
        {
            if (sentences[i] != null && sentences[i].IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                return i;
        }
        return -1;
    }

    public static void Main()
    {
        string[] s = { "I like apples.", "The quick brown fox.", "Example word here." };
        Console.WriteLine(IndexOfFirstSentenceWithWord(s, "fox")); // prints 1
    }
}