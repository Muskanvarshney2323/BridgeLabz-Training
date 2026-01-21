using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 10: Count Words in a File
/// Reads a text file and counts word occurrences.
/// Displays the top 5 most frequently occurring words.
/// </summary>
class WordCounterProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       Count Words in a File                       ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        string testFile = "word_count_file.txt";

        try
        {
            // Create a sample text file
            Console.WriteLine("Creating sample text file...");
            CreateSampleTextFile(testFile);
            Console.WriteLine($"✓ Created: {testFile}\n");

            // Count words and find top 5
            Console.WriteLine("=== Word Frequency Analysis ===\n");
            CountWordsAndDisplayTop5(testFile);

            Console.WriteLine("\n✓ Word counting completed successfully!");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"✗ File not found: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"✗ IO Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
        finally
        {
            // Cleanup
            if (File.Exists(testFile))
                File.Delete(testFile);
        }
    }

    static void CreateSampleTextFile(string filePath)
    {
        string content = @"The quick brown fox jumps over the lazy dog.
C# is a great programming language for developers.
Java and Python are also popular programming languages.
The C# ecosystem is very rich and powerful.
Developers love C# for its simplicity and efficiency.
Cloud development with C# is becoming more popular.
Microsoft provides excellent tools for C# development.
The .NET framework is a strong foundation for C# applications.
Azure supports C# applications for cloud computing.
C# developers are in high demand in the job market.
The syntax of C# is clean and easy to understand.
Functional programming features in C# are awesome.
Lambda expressions in C# make code more concise.
LINQ is a powerful feature for querying data in C#.
The C# community is very active and supportive.
Many Fortune 500 companies use C# for their applications.
Game development with C# using Unity is very popular.
Web development with C# using ASP.NET is widespread.
Desktop applications can be built efficiently with C#.
C# has been evolving consistently over the years.
Type safety in C# helps catch errors at compile time.
The garbage collector in C# manages memory efficiently.
Object-oriented programming in C# is very intuitive.
Async and await in C# make asynchronous programming easy.
Dependency injection is a core feature of ASP.NET Core in C#.
Unit testing in C# is straightforward with frameworks like NUnit.
Entity Framework simplifies database operations in C#.
Windows Forms and WPF are frameworks for desktop UI in C#.
The nuget package manager provides thousands of C# libraries.
Visual Studio is the best IDE for C# development.";

        File.WriteAllText(filePath, content);
    }

    static void CountWordsAndDisplayTop5(string filePath)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
        int totalWords = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            
            while ((line = reader.ReadLine()) != null)
            {
                // Extract words from line
                string[] words = line.Split(new[] { ' ', '.', ',', ';', '!', '?', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var word in words)
                {
                    totalWords++;
                    string lowerWord = word.ToLower();

                    if (wordFrequency.ContainsKey(lowerWord))
                    {
                        wordFrequency[lowerWord]++;
                    }
                    else
                    {
                        wordFrequency[lowerWord] = 1;
                    }
                }
            }
        }

        // Sort by frequency and get top 5
        var top5Words = wordFrequency
            .OrderByDescending(kvp => kvp.Value)
            .Take(5)
            .ToList();

        // Display results
        Console.WriteLine($"Total unique words: {wordFrequency.Count}");
        Console.WriteLine($"Total words: {totalWords}\n");

        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║      TOP 5 MOST FREQUENT WORDS       ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");

        int rank = 1;
        foreach (var kvp in top5Words)
        {
            double percentage = (kvp.Value * 100.0) / totalWords;
            Console.WriteLine($"{rank}. Word: \"{kvp.Key}\"");
            Console.WriteLine($"   Frequency: {kvp.Value} times");
            Console.WriteLine($"   Percentage: {percentage:F2}%");
            Console.WriteLine();
            rank++;
        }

        // Display all words sorted by frequency
        Console.WriteLine("\n--- All Words Sorted by Frequency ---");
        int count = 1;
        foreach (var kvp in wordFrequency.OrderByDescending(kvp => kvp.Value).Take(20))
        {
            Console.WriteLine($"{count}. {kvp.Key}: {kvp.Value}");
            count++;
        }
    }
}
