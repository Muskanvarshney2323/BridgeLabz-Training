using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CollectionRegex.Advanced
{
    /// <summary>
    /// Problem 12: Extract Programming Language Names from a Text
    /// Extracts known programming language names from text
    /// </summary>
    class ExtractProgrammingLanguages
    {
        static void Main()
        {
            Console.WriteLine("=== Extract Programming Language Names ===\n");

            // Define programming languages
            List<string> languages = new List<string>
            {
                "Java", "Python", "JavaScript", "C#", "C\\+\\+", "Go", "Rust",
                "PHP", "Ruby", "Swift", "Kotlin", "TypeScript", "Scala",
                "R", "MATLAB", "Perl", "Groovy", "Haskell", "Clojure"
            };

            // Test cases
            string[] texts = new string[]
            {
                "I love Java, Python, and JavaScript, but I haven't tried Go yet.",
                "C++ and Rust are powerful languages for system programming.",
                "I prefer Python over JavaScript for backend development.",
                "Java, C#, and Ruby are great for web development.",
                "Learn Go, Rust, and TypeScript for modern development.",
                "Swift is perfect for iOS, while Kotlin is for Android.",
                "MATLAB and R are popular for data science.",
                "No programming languages mentioned here!",
                "I code in Java and Python daily.",
            };

            Console.WriteLine($"Programming Languages to Extract: {string.Join(", ", languages)}\n");

            foreach (string text in texts)
            {
                Console.WriteLine($"Text: {text}");
                Console.WriteLine("Extracted Languages:");
                ExtractAndDisplayLanguages(text, languages);
                Console.WriteLine();
            }

            // Detailed analysis
            Console.WriteLine("\n--- Detailed Analysis ---\n");
            DetailedLanguageAnalysis();
        }

        static void ExtractAndDisplayLanguages(string text, List<string> languages)
        {
            List<string> foundLanguages = ExtractLanguages(text, languages);

            if (foundLanguages.Count == 0)
            {
                Console.WriteLine("  (no languages found)");
            }
            else
            {
                Console.WriteLine($"  {string.Join(", ", foundLanguages)}");
            }
        }

        static List<string> ExtractLanguages(string text, List<string> languages)
        {
            List<string> found = new List<string>();

            foreach (string language in languages)
            {
                // Case-insensitive search with word boundaries
                string pattern = @$"\b{language}\b";

                if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
                {
                    // Extract the actual matched text (preserve original case)
                    Match match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                    if (match.Success && !found.Contains(match.Value))
                    {
                        found.Add(match.Value);
                    }
                }
            }

            return found;
        }

        static void DetailedLanguageAnalysis()
        {
            List<string> languages = new List<string>
            {
                "Java", "Python", "JavaScript", "C#", "C\\+\\+", "Go"
            };

            string text = "I love Java, Python, and JavaScript. Go is great for backend, and C# is fantastic for web apps.";

            Console.WriteLine($"Text: {text}\n");

            List<string> foundLanguages = ExtractLanguages(text, languages);

            Console.WriteLine($"Languages Found: {foundLanguages.Count}\n");

            for (int i = 0; i < foundLanguages.Count; i++)
            {
                string lang = foundLanguages[i];
                Console.WriteLine($"  {i + 1}. {lang}");
            }

            // Show what type of language each is
            Console.WriteLine("\n--- Language Classifications ---\n");
            DisplayLanguageClassifications(foundLanguages);
        }

        static void DisplayLanguageClassifications(List<string> languages)
        {
            var classifications = new Dictionary<string, string>
            {
                { "Java", "Object-Oriented, Compiled" },
                { "Python", "Interpreted, Dynamic" },
                { "JavaScript", "Interpreted, Dynamic, Web" },
                { "C#", "Object-Oriented, Compiled" },
                { "C++", "Object-Oriented, Compiled" },
                { "Go", "Compiled, Statically Typed" },
                { "Rust", "Compiled, Memory-Safe" },
                { "PHP", "Interpreted, Web" },
                { "Ruby", "Interpreted, Dynamic" },
                { "Swift", "Compiled, iOS/macOS" },
                { "Kotlin", "JVM, Android" },
                { "TypeScript", "Superset of JavaScript" },
            };

            foreach (string lang in languages)
            {
                if (classifications.ContainsKey(lang))
                {
                    Console.WriteLine($"{lang,-15} - {classifications[lang]}");
                }
            }
        }

        // Count language mentions
        static Dictionary<string, int> CountLanguageMentions(string text, List<string> languages)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach (string language in languages)
            {
                string pattern = @$"\b{language}\b";
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
                
                if (matches.Count > 0)
                {
                    counts[language] = matches.Count;
                }
            }

            return counts;
        }

        // Extract with context (words around the language name)
        static Dictionary<string, List<string>> ExtractLanguagesWithContext(string text, List<string> languages, int contextWords = 2)
        {
            Dictionary<string, List<string>> contextDict = new Dictionary<string, List<string>>();

            foreach (string language in languages)
            {
                string pattern = @$"\b{language}\b";
                MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

                if (matches.Count > 0)
                {
                    List<string> contexts = new List<string>();

                    foreach (Match match in matches)
                    {
                        // Get context around the match
                        int start = Math.Max(0, match.Index - 20);
                        int length = Math.Min(text.Length - start, 40 + language.Length);
                        string context = text.Substring(start, length).Trim();
                        contexts.Add(context);
                    }

                    contextDict[language] = contexts;
                }
            }

            return contextDict;
        }

        // Display language statistics
        static void DisplayLanguageStatistics()
        {
            Console.WriteLine("\n\n--- Language Statistics ---\n");

            List<string> languages = new List<string>
            {
                "Java", "Python", "JavaScript", "C#", "C\\+\\+", "Go"
            };

            string text = "I program in Java and Python daily. Java is powerful, and Python is simple. JavaScript is essential for web. " +
                         "C# is my favorite, and C++ is my second choice. Go is growing in popularity.";

            var counts = CountLanguageMentions(text, languages);

            Console.WriteLine("Language Mention Counts:");
            foreach (var kvp in counts)
            {
                Console.WriteLine($"  {kvp.Key,-15} : {kvp.Value} mention(s)");
            }

            int total = 0;
            foreach (var kvp in counts)
            {
                total += kvp.Value;
            }
            Console.WriteLine($"\nTotal mentions: {total}");
        }
    }
}
