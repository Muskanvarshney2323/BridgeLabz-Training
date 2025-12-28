using System;
class LongestWord
{
    static void Main()
    {
        Console.Write("Enter a sentence: ");
        string sentence = Console.ReadLine() ?? string.Empty;
        string longest = FindLongestWordMethod(sentence);
        Console.WriteLine("Longest word: " + longest);
    }

    public static string FindLongestWordMethod(string sentence)
    {
        string[] parts = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string longest = string.Empty;
        foreach (string w in parts)
        {
            var sb = new System.Text.StringBuilder();
            foreach (char c in w)
            {
                if (char.IsLetter(c)) sb.Append(c);
            }
            string cleaned = sb.ToString();
            if (cleaned.Length > longest.Length) longest = cleaned;
        }
        return longest;
    }
}