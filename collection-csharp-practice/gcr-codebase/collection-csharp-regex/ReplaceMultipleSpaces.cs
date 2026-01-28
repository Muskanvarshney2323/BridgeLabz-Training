using System;
using System.Text.RegularExpressions;

namespace CollectionRegex.Replace
{
    /// <summary>
    /// Problem 8: Replace Multiple Spaces with a Single Space
    /// Removes extra spaces and keeps only single spaces between words
    /// </summary>
    class ReplaceMultipleSpaces
    {
        static void Main()
        {
            Console.WriteLine("=== Replace Multiple Spaces with Single Space ===\n");

            // Test cases
            string[] testCases = new string[]
            {
                "This is an example with multiple spaces.",
                "This   has   three   spaces.",
                "Text    with     varying     space     counts.",
                "Single  space  test",
                "Tabs\tand\tmultiple    spaces",
                "Leading    and    trailing    spaces   ",
                "   Multiple  spaces    at   start   and    end   ",
                "Normal text with standard spacing.",
                "Multiple     consecutive     spaces     here.",
            };

            Console.WriteLine("{0,-50} {1,-50}", "Original", "Result");
            Console.WriteLine(new string('-', 100));

            foreach (string text in testCases)
            {
                string normalized = NormalizeSpaces(text);
                Console.WriteLine("{0,-50} {1,-50}", $"\"{text}\"", $"\"{normalized}\"");
            }

            // Detailed example
            Console.WriteLine("\n\n--- Detailed Space Normalization ---\n");
            DetailedNormalization();
        }

        static string NormalizeSpaces(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Pattern explanation:
            // \s+  - One or more whitespace characters (spaces, tabs, etc.)
            // Replaced with a single space

            string normalized = Regex.Replace(text, @"\s+", " ");
            
            // Remove leading and trailing spaces
            return normalized.Trim();
        }

        static void DetailedNormalization()
        {
            string[] examples = new string[]
            {
                "This   is   a   test.",
                "Text    with    tabs\tand    spaces.",
                "  Leading and trailing spaces  ",
                "Multiple    consecutive    spaces",
            };

            foreach (string example in examples)
            {
                Console.WriteLine($"Original: \"{example}\"");
                Console.WriteLine($"Spaces count: {CountConsecutiveSpaces(example)}");
                Console.WriteLine($"Normalized: \"{NormalizeSpaces(example)}\"");
                Console.WriteLine();
            }
        }

        static string CountConsecutiveSpaces(string text)
        {
            // Count spaces in different groups
            var matches = Regex.Matches(text, @"\s{2,}");
            if (matches.Count == 0)
                return "No multiple spaces";

            string result = "";
            foreach (Match match in matches)
            {
                result += $"{match.Value.Length} spaces, ";
            }
            return result.TrimEnd(',', ' ');
        }

        // Alternative: Replace with specific number of spaces
        static string ReplaceMultipleSpacesWithN(string text, int spaceCount)
        {
            string spaces = new string(' ', spaceCount);
            return Regex.Replace(text, @"\s+", spaces);
        }

        // Alternative: Replace only multiple spaces (keep single spaces)
        static string ReplaceOnlyMultipleSpaces(string text)
        {
            // This pattern specifically targets 2 or more spaces
            return Regex.Replace(text, @" {2,}", " ");
        }

        // Preserve line breaks
        static string NormalizeSpacesPreservingLineBreaks(string text)
        {
            // Replace multiple spaces/tabs with single space, but preserve newlines
            string pattern = @"[ \t]+";
            return Regex.Replace(text, pattern, " ");
        }

        // Replace multiple spaces with specific formatting
        static void DemonstrateVariations()
        {
            Console.WriteLine("\n\n--- Different Space Replacement Strategies ---\n");

            string text = "This    has   multiple    spaces    here.";

            Console.WriteLine($"Original: \"{text}\"\n");

            Console.WriteLine($"Replace with 1 space: \"{NormalizeSpaces(text)}\"");
            Console.WriteLine($"Replace with 2 spaces: \"{ReplaceMultipleSpacesWithN(text, 2)}\"");
            Console.WriteLine($"Replace with tab: \"{Regex.Replace(text, @"\s+", "\t")}\"");
            Console.WriteLine($"Only 2+ spaces: \"{ReplaceOnlyMultipleSpaces(text)}\"");
        }

        // Count statistics
        static void DisplayStatistics(string text)
        {
            Console.WriteLine("\n\n--- Statistics ---\n");

            int totalSpaces = Regex.Matches(text, @" ").Count;
            int singleSpaces = Regex.Matches(text, @"(?<! ) (?! )").Count;
            var multipleSpaces = Regex.Matches(text, @" {2,}");

            Console.WriteLine($"Original text: \"{text}\"");
            Console.WriteLine($"Total spaces: {totalSpaces}");
            Console.WriteLine($"Single spaces: {singleSpaces}");
            Console.WriteLine($"Multiple space groups: {multipleSpaces.Count}");

            if (multipleSpaces.Count > 0)
            {
                Console.WriteLine("Groups with multiple spaces:");
                foreach (Match match in multipleSpaces)
                {
                    Console.WriteLine($"  - {match.Value.Length} spaces");
                }
            }

            string normalized = NormalizeSpaces(text);
            Console.WriteLine($"\nNormalized: \"{normalized}\"");
            Console.WriteLine($"Original length: {text.Length}");
            Console.WriteLine($"Normalized length: {normalized.Length}");
            Console.WriteLine($"Reduction: {text.Length - normalized.Length} characters");
        }
    }
}
