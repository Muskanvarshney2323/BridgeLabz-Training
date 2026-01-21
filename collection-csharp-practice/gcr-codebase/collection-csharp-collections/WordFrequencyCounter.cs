using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Problem 1: Word Frequency Counter
/// Read a text file and count the frequency of each word using a Dictionary<string, int>.
/// Example:
/// Input: "Hello world, hello Java!"
/// Output: { "hello": 2, "world": 1, "java": 1 }
/// </summary>
class WordFrequencyCounterProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       Word Frequency Counter                      ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Simple text
            Console.WriteLine("=== Test Case 1: Simple Text ===");
            string text1 = "Hello world hello Java hello C#";
            Console.WriteLine($"Input: \"{text1}\"");
            
            Dictionary<string, int> frequency1 = CountWordFrequency(text1);
            Console.WriteLine("Word Frequency:");
            DisplayFrequency(frequency1);
            Console.WriteLine();

            // Test case 2: Text with punctuation
            Console.WriteLine("=== Test Case 2: Text with Punctuation ===");
            string text2 = "Hello, world! Hello Java. Hello, C#!";
            Console.WriteLine($"Input: \"{text2}\"");
            
            Dictionary<string, int> frequency2 = CountWordFrequency(text2);
            Console.WriteLine("Word Frequency:");
            DisplayFrequency(frequency2);
            Console.WriteLine();

            // Test case 3: File-based word counting
            Console.WriteLine("=== Test Case 3: File-Based Word Counting ===");
            string testFile = "sample_text.txt";
            
            // Create sample file
            string fileContent = @"C# is a powerful programming language.
C# is widely used in enterprise applications.
Programming in C# is productive and efficient.
C# provides excellent libraries and tools.
The C# community is very active and supportive.";

            File.WriteAllText(testFile, fileContent);
            Console.WriteLine($"Created file: {testFile}");
            Console.WriteLine($"File content:\n{fileContent}\n");
            
            Dictionary<string, int> frequency3 = CountWordFrequencyFromFile(testFile);
            Console.WriteLine("Word Frequency (Top 10):");
            var top10 = frequency3.OrderByDescending(x => x.Value).Take(10);
            DisplayFrequency(top10.ToDictionary(x => x.Key, x => x.Value));
            
            // Cleanup
            File.Delete(testFile);
            Console.WriteLine();

            // Test case 4: Case sensitivity demonstration
            Console.WriteLine("=== Test Case 4: Case Insensitive Counting ===");
            string text4 = "Java Java JAVA java java";
            Console.WriteLine($"Input: \"{text4}\"");
            Console.WriteLine("Case-insensitive frequency:");
            
            Dictionary<string, int> frequency4 = CountWordFrequency(text4);
            DisplayFrequency(frequency4);
            Console.WriteLine();

            Console.WriteLine("✓ Word frequency counting completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static Dictionary<string, int> CountWordFrequency(string text)
    {
        Dictionary<string, int> frequency = new Dictionary<string, int>();

        // Remove punctuation and convert to lowercase
        string[] separators = { " ", ",", ".", "!", "?", ";", ":", "-", "\n", "\t" };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            string lowerWord = word.ToLower();

            if (frequency.ContainsKey(lowerWord))
            {
                frequency[lowerWord]++;
            }
            else
            {
                frequency[lowerWord] = 1;
            }
        }

        return frequency;
    }

    static Dictionary<string, int> CountWordFrequencyFromFile(string filePath)
    {
        Dictionary<string, int> frequency = new Dictionary<string, int>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] separators = { " ", ",", ".", "!", "?", ";", ":", "-" };
                    string[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        string lowerWord = word.ToLower();

                        if (frequency.ContainsKey(lowerWord))
                        {
                            frequency[lowerWord]++;
                        }
                        else
                        {
                            frequency[lowerWord] = 1;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return frequency;
    }

    static void DisplayFrequency(Dictionary<string, int> frequency)
    {
        var sorted = frequency.OrderByDescending(x => x.Value);
        foreach (var kvp in sorted)
        {
            Console.WriteLine($"  \"{kvp.Key}\": {kvp.Value}");
        }
    }
}
