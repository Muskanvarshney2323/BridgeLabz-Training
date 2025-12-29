using System;

class ParagraphAnalyzer
{
    static void Main()
    {
        Console.WriteLine("Enter a paragraph:"); //user input
        string paragraph = Console.ReadLine();

        Console.WriteLine("Enter word to replace:");
        string oldWord = Console.ReadLine();

        Console.WriteLine("Enter new word:");
        string newWord = Console.ReadLine();

        // empty input
        if (string.IsNullOrWhiteSpace(paragraph))
        {
            Console.WriteLine("Paragraph is empty or contains only spaces.");
            return;
        }

        // Remove extra spaces
        paragraph = paragraph.Trim();
        while (paragraph.Contains("  "))
        {
            paragraph = paragraph.Replace("  ", " ");
        }

        // count words
        string[] words = paragraph.Split(' ');

        Console.WriteLine("Word Count: " + words.Length);

        // longest word
        string longestWord = words[0];
        for (int i = 1; i < words.Length; i++)
        {
            if (words[i].Length > longestWord.Length)
            {
                longestWord = words[i];
            }
        }
        Console.WriteLine("Longest Word: " + longestWord);

        // Replace word
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].ToLower() == oldWord.ToLower())
            {
                words[i] = newWord;
            }
        }

        // Join the words back to the string
        string updatedParagraph = string.Join(" ", words);

        Console.WriteLine("Updated Paragraph:");
        Console.WriteLine(updatedParagraph);
    }
}
