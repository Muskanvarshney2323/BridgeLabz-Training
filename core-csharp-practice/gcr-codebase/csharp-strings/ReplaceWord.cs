using System;
class ReplaceWord
{
    static void Main()
    {
        Console.Write("Enter a sentence: ");
        string sentence = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter the word to replace: ");
        string oldWord = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter the new word: ");
        string newWord = Console.ReadLine() ?? string.Empty;
        string result = ReplaceWordMethod(sentence, oldWord, newWord);
        Console.WriteLine("Modified sentence: " + result);
    }

   
    public static string ReplaceWordMethod(string sentence, string oldWord, string newWord)
    {
        if (string.IsNullOrEmpty(oldWord)) return sentence;
        string[] part = sentence.Split(' ');
        for (int i = 0; i < part.Length; i++)
        {
            // Strip punctuation from start/end for matching, preserve punctuation
            string token = part[i];
            int start = 0, end = token.Length - 1;
            while (start <= end && !char.IsLetterOrDigit(token[start])) start++;
            while (end >= start && !char.IsLetterOrDigit(token[end])) end--;
            if (start > end) continue;
            string core = token.Substring(start, end - start + 1);
            if (core == oldWord)
            {
                // rebuild token with punctuation preserved
                part[i] = token.Substring(0, start) + newWord + token.Substring(end + 1);
            }
        }
        return string.Join(' ', part);
    }
}