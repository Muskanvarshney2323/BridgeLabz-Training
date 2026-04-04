using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Replace
{
    /// <summary>
    /// Problem 9: Censor Bad Words in a Sentence
    /// Replaces specified bad words with ****
    /// </summary>
    class CensorBadWords
    {
        static void Main()
        {
            Console.WriteLine("=== Censor Bad Words ===\n");

            // Define bad words
            List<string> badWords = new List<string>
            {
                "damn",
                "stupid",
                "badword",
                "awful"
            };

            // Test sentences
            string[] testSentences = new string[]
            {
                "This is a damn bad example with some stupid words.",
                "What a stupid and awful day this is!",
                "This damn code is so stupid.",
                "I think that's a badword and totally awful.",
                "This is a normal sentence without any issues.",
                "Damn, stupid, and badword in the same sentence!",
                "STUPID, Stupid, stupid - all should be censored.",
                "This contains damn and DAMN - both should be censored.",
            };

            Console.WriteLine($"Bad Words to Censor: {string.Join(", ", badWords)}\n");

            Console.WriteLine("{0,-50} {1,-50}", "Original", "Censored");
            Console.WriteLine(new string('-', 100));

            foreach (string sentence in testSentences)
            {
                string censored = CensorWords(sentence, badWords);
                Console.WriteLine("{0,-50} {1,-50}", sentence, censored);
            }

            // Detailed example
            Console.WriteLine("\n\n--- Detailed Censoring Analysis ---\n");
            DetailedAnalysis(badWords);
        }

        static string CensorWords(string text, List<string> badWords)
        {
            string result = text;

            foreach (string badWord in badWords)
            {
                // Pattern explanation:
                // (?i)              - Case-insensitive flag
                // \b                - Word boundary
                // badWord           - The word to match
                // \b                - Word boundary

                string pattern = @$"(?i)\b{Regex.Escape(badWord)}\b";
                result = Regex.Replace(result, pattern, "****", RegexOptions.IgnoreCase);
            }

            return result;
        }

        static void DetailedAnalysis(List<string> badWords)
        {
            string text = "This is a damn bad example with some stupid words. What a stupid thing!";

            Console.WriteLine($"Original text:\n  {text}\n");

            // Show step-by-step censoring
            string result = text;
            Console.WriteLine("Step-by-step censoring:");

            foreach (string badWord in badWords)
            {
                string pattern = @$"(?i)\b{Regex.Escape(badWord)}\b";
                MatchCollection matches = Regex.Matches(result, pattern, RegexOptions.IgnoreCase);

                if (matches.Count > 0)
                {
                    Console.WriteLine($"  Found '{badWord}': {matches.Count} occurrence(s)");
                    result = Regex.Replace(result, pattern, "****", RegexOptions.IgnoreCase);
                    Console.WriteLine($"  Result: {result}");
                }
                else
                {
                    Console.WriteLine($"  No match for '{badWord}'");
                }
            }

            Console.WriteLine($"\nFinal censored text:\n  {result}");
        }

        // Censor with different replacement strings
        static string CensorWordsCustomReplacement(string text, List<string> badWords, string replacement)
        {
            string result = text;

            foreach (string badWord in badWords)
            {
                string pattern = @$"(?i)\b{Regex.Escape(badWord)}\b";
                result = Regex.Replace(result, pattern, replacement, RegexOptions.IgnoreCase);
            }

            return result;
        }

        // Count bad words
        static Dictionary<string, int> CountBadWords(string text, List<string> badWords)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach (string badWord in badWords)
            {
                string pattern = @$"(?i)\b{Regex.Escape(badWord)}\b";
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
                counts[badWord] = matches.Count;
            }

            return counts;
        }

        // Demonstrate different replacement styles
        static void DemonstrateDifferentReplacements()
        {
            Console.WriteLine("\n\n--- Different Replacement Styles ---\n");

            List<string> badWords = new List<string> { "damn", "stupid" };
            string text = "This is a damn bad example with some stupid words.";

            Console.WriteLine($"Original: {text}\n");

            Console.WriteLine($"Replace with ****:\n  {CensorWordsCustomReplacement(text, badWords, "****")}\n");
            Console.WriteLine($"Replace with *:\n  {CensorWordsCustomReplacement(text, badWords, "*")}\n");
            Console.WriteLine($"Replace with [CENSORED]:\n  {CensorWordsCustomReplacement(text, badWords, "[CENSORED]")}\n");
            Console.WriteLine($"Replace with ###:\n  {CensorWordsCustomReplacement(text, badWords, "###")}\n");
        }

        // Case-sensitive vs case-insensitive
        static void DemonstrateCaseSensitivity()
        {
            Console.WriteLine("\n\n--- Case Sensitivity ---\n");

            List<string> badWords = new List<string> { "stupid" };
            string text = "This is Stupid, STUPID, and stupid.";

            Console.WriteLine($"Original: {text}\n");

            // Case-insensitive (current implementation)
            Console.WriteLine($"Case-insensitive: {CensorWords(text, badWords)}\n");

            // Could also implement case-sensitive version
            string caseInsensitiveResult = Regex.Replace(text, @"\bstupid\b", "****", RegexOptions.IgnoreCase);
            string caseSensitiveResult = Regex.Replace(text, @"\bstupid\b", "****");

            Console.WriteLine($"Case-insensitive replacement: {caseInsensitiveResult}");
            Console.WriteLine($"Case-sensitive replacement: {caseSensitiveResult}");
        }

        // Partial word matching
        static string CensorPartialMatches(string text, List<string> badWords)
        {
            string result = text;

            foreach (string badWord in badWords)
            {
                // Without word boundaries - matches partial words too
                result = Regex.Replace(result, badWord, "****", RegexOptions.IgnoreCase);
            }

            return result;
        }

        // Statistics about bad words
        static void DisplayBadWordsStatistics()
        {
            Console.WriteLine("\n\n--- Bad Words Statistics ---\n");

            List<string> badWords = new List<string> { "damn", "stupid", "awful" };
            string text = "This is a damn bad example with some stupid words. What an awful day! Damn, stupid, and awful!";

            Console.WriteLine($"Text: {text}\n");

            var counts = CountBadWords(text, badWords);

            Console.WriteLine("Bad word counts:");
            int total = 0;
            foreach (var kvp in counts)
            {
                Console.WriteLine($"  '{kvp.Key}': {kvp.Value} occurrence(s)");
                total += kvp.Value;
            }

            Console.WriteLine($"\nTotal bad words: {total}");
            Console.WriteLine($"Censored text: {CensorWords(text, badWords)}");
        }
    }
}
