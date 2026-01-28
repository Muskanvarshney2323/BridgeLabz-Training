using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Extraction
{
    /// <summary>
    /// Problem 5: Extract All Capitalized Words from a Sentence
    /// Extracts words that start with an uppercase letter
    /// </summary>
    class ExtractCapitalizedWords
    {
        static void Main()
        {
            Console.WriteLine("=== Extract Capitalized Words ===\n");

            // Test cases
            string[] sentences = new string[]
            {
                "The Eiffel Tower is in Paris and the Statue of Liberty is in New York.",
                "Java and Python are popular programming languages.",
                "Alice met Bob at the library yesterday.",
                "The Quick Brown Fox Jumps Over The Lazy Dog.",
                "john is learning C# at the university.",
                "NASA launched the Hubble Space Telescope in 1990.",
                "I love visiting London, Paris, and Rome.",
                "The book 'To Kill a Mockingbird' was written by Harper Lee.",
            };

            foreach (string sentence in sentences)
            {
                Console.WriteLine($"Sentence: {sentence}");
                Console.WriteLine("Capitalized Words:");
                ExtractAndDisplayCapitalizedWords(sentence);
                Console.WriteLine();
            }

            // Detailed example
            Console.WriteLine("\n--- Detailed Analysis ---\n");
            DetailedCapitalizedWordAnalysis();
        }

        static void ExtractAndDisplayCapitalizedWords(string sentence)
        {
            List<string> capitalizedWords = ExtractCapitalizedWords(sentence);

            if (capitalizedWords.Count == 0)
            {
                Console.WriteLine("  (no capitalized words found)");
            }
            else
            {
                Console.WriteLine($"  {string.Join(", ", capitalizedWords)}");
                Console.WriteLine($"  (Total: {capitalizedWords.Count})");
            }
        }

        static List<string> ExtractCapitalizedWords(string sentence)
        {
            List<string> words = new List<string>();

            // Regex pattern explanation:
            // \b          - Word boundary
            // [A-Z]       - Starts with uppercase letter
            // [a-zA-Z]*   - Followed by zero or more letters (any case)
            // \b          - Word boundary

            string pattern = @"\b[A-Z][a-zA-Z]*\b";

            MatchCollection matches = Regex.Matches(sentence, pattern);

            foreach (Match match in matches)
            {
                words.Add(match.Value);
            }

            return words;
        }

        static void DetailedCapitalizedWordAnalysis()
        {
            string sentence = "The Eiffel Tower is in Paris and the Statue of Liberty is in New York.";

            Console.WriteLine($"Sentence: {sentence}\n");

            List<string> capitalizedWords = ExtractCapitalizedWords(sentence);

            Console.WriteLine($"Capitalized Words Found: {capitalizedWords.Count}\n");

            for (int i = 0; i < capitalizedWords.Count; i++)
            {
                string word = capitalizedWords[i];
                Console.WriteLine($"  {i + 1}. {word,-15} Length: {word.Length,-3} Type: " +
                                 $"{GetWordType(word)}");
            }

            // Statistics
            Console.WriteLine($"\n--- Statistics ---");
            Console.WriteLine($"Total Words: {GetTotalWords(sentence)}");
            Console.WriteLine($"Capitalized Words: {capitalizedWords.Count}");
            Console.WriteLine($"Percentage: {(double)capitalizedWords.Count / GetTotalWords(sentence) * 100:F2}%");
        }

        static int GetTotalWords(string sentence)
        {
            string pattern = @"\b\w+\b";
            MatchCollection matches = Regex.Matches(sentence, pattern);
            return matches.Count;
        }

        static string GetWordType(string word)
        {
            if (word.All(c => char.IsUpper(c)))
                return "ACRONYM";
            else if (char.IsUpper(word[0]))
                return "ProperNoun/Capitalized";
            else
                return "Regular";
        }

        // Extract proper nouns (more sophisticated)
        static List<string> ExtractProperNouns(string sentence)
        {
            List<string> properNouns = new List<string>();

            // This pattern looks for capitalized words that are NOT at the beginning of a sentence
            string[] words = sentence.Split(' ');
            bool isFirstWord = true;

            foreach (string word in words)
            {
                // Remove punctuation
                string cleanWord = Regex.Replace(word, @"[^\w]", "");

                if (!string.IsNullOrEmpty(cleanWord) && 
                    char.IsUpper(cleanWord[0]) && 
                    !isFirstWord)
                {
                    properNouns.Add(cleanWord);
                }

                isFirstWord = false;

                // Reset for new sentences
                if (word.EndsWith(".") || word.EndsWith("!") || word.EndsWith("?"))
                {
                    isFirstWord = true;
                }
            }

            return properNouns;
        }

        // Extract different types of capitalized patterns
        static void DemonstrateDifferentPatterns()
        {
            Console.WriteLine("\n\n--- Different Capitalization Patterns ---\n");

            string text = "The UN approved the NATO decision. Dr. Smith and Ms. Johnson attended the meeting.";

            Console.WriteLine($"Text: {text}\n");

            // Simple capitalized words
            Console.WriteLine("Pattern 1: Any word starting with uppercase");
            var pattern1 = ExtractCapitalizedWords(text);
            Console.WriteLine($"Results: {string.Join(", ", pattern1)}\n");

            // All uppercase words (acronyms)
            Console.WriteLine("Pattern 2: All uppercase words (Acronyms)");
            string acronymPattern = @"\b[A-Z]{2,}\b";
            MatchCollection acronyms = Regex.Matches(text, acronymPattern);
            foreach (Match match in acronyms)
            {
                Console.WriteLine($"  {match.Value}");
            }

            // Title case words (first letter uppercase)
            Console.WriteLine("\nPattern 3: Title case words");
            string titlePattern = @"\b[A-Z][a-z]+\b";
            MatchCollection titleWords = Regex.Matches(text, titlePattern);
            foreach (Match match in titleWords)
            {
                Console.WriteLine($"  {match.Value}");
            }
        }
    }
}
