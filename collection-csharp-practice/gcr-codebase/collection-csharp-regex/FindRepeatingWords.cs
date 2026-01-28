using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Advanced
{
    /// <summary>
    /// Problem 14: Find Repeating Words in a Sentence
    /// Uses regex with backreferences to find consecutive duplicate words
    /// </summary>
    class FindRepeatingWords
    {
        static void Main()
        {
            Console.WriteLine("=== Find Repeating Words in Sentences ===\n");

            // Test cases
            string[] sentences = new string[]
            {
                "I love love programming!",
                "The the problem is is this this issue.",
                "She went to to the store store yesterday.",
                "What what are are you doing doing?",
                "Hello world, how how are you?",
                "No repeating words here.",
                "This is is a test test of the the system.",
                "Sometimes people people make make mistakes mistakes.",
                "Can can can you hear me me me?",
                "She she she loves loves loves coding coding.",
            };

            Console.WriteLine("Test Cases:\n");

            foreach (string sentence in sentences)
            {
                Console.WriteLine($"Sentence: {sentence}");
                Console.WriteLine("Repeating Words:");
                ExtractAndDisplayRepeatingWords(sentence);
                Console.WriteLine();
            }

            // Detailed analysis
            Console.WriteLine("\n--- Detailed Analysis ---\n");
            DetailedRepeatingAnalysis();

            // Find patterns with triple or more occurrences
            Console.WriteLine("\n--- Triple+ Repetitions ---\n");
            FindTripleRepetitions();

            // Analyze frequency
            Console.WriteLine("\n--- Repetition Analysis ---\n");
            AnalyzeRepetitionFrequency();
        }

        static void ExtractAndDisplayRepeatingWords(string sentence)
        {
            // Pattern: captures a word, then matches the same word again with optional whitespace between
            string pattern = @"\b(\w+)\s+(?=\1\b)";

            MatchCollection matches = Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase);

            if (matches.Count == 0)
            {
                Console.WriteLine("  (no repeating words found)");
            }
            else
            {
                HashSet<string> uniqueRepeating = new HashSet<string>();
                foreach (Match match in matches)
                {
                    uniqueRepeating.Add(match.Groups[1].Value.ToLower());
                }

                foreach (string word in uniqueRepeating)
                {
                    Console.WriteLine($"  ✓ {word}");
                }
            }
        }

        static List<string> ExtractRepeatingWords(string sentence)
        {
            string pattern = @"\b(\w+)\s+(?=\1\b)";
            MatchCollection matches = Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase);

            List<string> results = new List<string>();
            foreach (Match match in matches)
            {
                string word = match.Groups[1].Value.ToLower();
                if (!results.Contains(word))
                {
                    results.Add(word);
                }
            }

            return results;
        }

        static void DetailedRepeatingAnalysis()
        {
            string sentence = "I think think we need need to go go quickly quickly now.";

            Console.WriteLine($"Sentence: {sentence}\n");

            List<string> repeatingWords = ExtractRepeatingWords(sentence);

            Console.WriteLine($"Repeating Words Found: {repeatingWords.Count}\n");

            for (int i = 0; i < repeatingWords.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {repeatingWords[i]}");
            }

            // Show with position
            Console.WriteLine("\n--- Positions in Text ---\n");

            string pattern = @"\b(\w+)\s+(?=\1\b)";
            MatchCollection matches = Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase);

            int count = 1;
            foreach (Match match in matches)
            {
                Console.WriteLine($"  {count}. Word: '{match.Groups[1].Value}' at position {match.Index}");
                count++;
            }
        }

        static void FindTripleRepetitions()
        {
            string[] sentences = new string[]
            {
                "Go go go! Run run run!",
                "Yes yes yes, I agree agree agree.",
                "This this this is is is happening.",
                "No no no no no!",
                "Maybe maybe maybe we we we should should should try.",
            };

            // Pattern for triple or more repetitions
            string triplePattern = @"\b(\w+)\s+\1\s+\1";
            string quadPattern = @"\b(\w+)\s+\1\s+\1\s+\1";

            foreach (string sentence in sentences)
            {
                Console.WriteLine($"Sentence: {sentence}");

                MatchCollection tripleMatches = Regex.Matches(sentence, triplePattern, RegexOptions.IgnoreCase);
                MatchCollection quadMatches = Regex.Matches(sentence, quadPattern, RegexOptions.IgnoreCase);

                if (tripleMatches.Count > 0)
                {
                    Console.WriteLine($"  Triple+: {string.Join(", ", ExtractUniqueWords(tripleMatches))}");
                }

                if (quadMatches.Count > 0)
                {
                    Console.WriteLine($"  Quad+: {string.Join(", ", ExtractUniqueWords(quadMatches))}");
                }

                if (tripleMatches.Count == 0 && quadMatches.Count == 0)
                {
                    Console.WriteLine("  (no triple+ repetitions)");
                }

                Console.WriteLine();
            }
        }

        static List<string> ExtractUniqueWords(MatchCollection matches)
        {
            HashSet<string> words = new HashSet<string>();
            foreach (Match match in matches)
            {
                words.Add(match.Groups[1].Value.ToLower());
            }

            return new List<string>(words);
        }

        static void AnalyzeRepetitionFrequency()
        {
            string text = "The the dog dog and and cat cat are are running running. " +
                         "The the cat cat is is fast fast and and strong strong.";

            Console.WriteLine($"Text: {text}\n");

            // Count how many times each word is repeated
            string pattern = @"\b(\w+)\s+\1\b";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            Dictionary<string, int> repeatCounts = new Dictionary<string, int>();

            foreach (Match match in matches)
            {
                string word = match.Groups[1].Value.ToLower();
                if (repeatCounts.ContainsKey(word))
                {
                    repeatCounts[word]++;
                }
                else
                {
                    repeatCounts[word] = 1;
                }
            }

            Console.WriteLine("Repetition Frequency:\n");

            foreach (var kvp in repeatCounts)
            {
                Console.WriteLine($"  '{kvp.Key}' repeated {kvp.Value} time(s)");
            }

            int totalRepetitions = 0;
            foreach (var kvp in repeatCounts)
            {
                totalRepetitions += kvp.Value;
            }

            Console.WriteLine($"\nTotal repetitions: {totalRepetitions}");
        }

        // Count consecutive duplicates (not including lookahead)
        static Dictionary<string, int> CountConsecutiveDuplicates(string text)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            // This pattern captures the actual repeated instance
            string pattern = @"\b(\w+)\s+\1\b";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                string word = match.Groups[1].Value.ToLower();
                counts[word] = counts.ContainsKey(word) ? counts[word] + 1 : 1;
            }

            return counts;
        }

        // Remove repeating words
        static string RemoveRepeatingWords(string text)
        {
            return Regex.Replace(text, @"\b(\w+)\s+\1\b", "$1", RegexOptions.IgnoreCase);
        }

        // Highlight repeating words
        static void HighlightRepeatingWords()
        {
            Console.WriteLine("\n--- Highlight Repeating Words ---\n");

            string sentence = "I think think the problem problem is we we need need to focus focus.";
            Console.WriteLine($"Original: {sentence}");

            // Find and display with highlighting
            string pattern = @"\b(\w+)\s+\1\b";
            MatchCollection matches = Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase);

            string highlighted = sentence;
            foreach (Match match in matches)
            {
                highlighted = highlighted.Replace(match.Value, $"[{match.Value}]");
            }

            Console.WriteLine($"Highlighted: {highlighted}");

            // Show cleaned version
            string cleaned = RemoveRepeatingWords(sentence);
            Console.WriteLine($"Cleaned: {cleaned}");
        }

        // Find case-insensitive repeating words
        static void CaseInsensitiveAnalysis()
        {
            Console.WriteLine("\n--- Case-Insensitive Repeating Words ---\n");

            string[] sentences = new string[]
            {
                "The THE problem is IS critical CRITICAL.",
                "Hello HELLO there there THERE.",
                "This THIS is IS important IMPORTANT.",
            };

            foreach (string sentence in sentences)
            {
                Console.WriteLine($"Sentence: {sentence}");

                List<string> repeating = ExtractRepeatingWords(sentence);
                if (repeating.Count > 0)
                {
                    Console.WriteLine($"  Repeating: {string.Join(", ", repeating)}");
                }
                else
                {
                    Console.WriteLine("  (no repeating words)");
                }

                Console.WriteLine();
            }
        }
    }
}
