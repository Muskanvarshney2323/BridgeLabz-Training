using System;
using System.Collections.Generic;

/// <summary>
/// Problem 2: Find Frequency of Elements
/// Given a list of strings, count the frequency of each element and return results in a Dictionary.
/// Example:
/// Input: {"apple", "banana", "apple", "orange"}
/// Output: { "apple": 2, "banana": 1, "orange": 1 }
/// </summary>
class FrequencyCounterProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║      Find Frequency of Elements                   ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Fruits
            Console.WriteLine("=== Test Case 1: Fruit Frequency ===");
            List<string> fruits = new List<string> { "apple", "banana", "apple", "orange", "banana", "apple" };
            Console.WriteLine($"Input: [{string.Join(", ", fruits)}]");
            
            Dictionary<string, int> fruitFrequency = CountFrequency(fruits);
            Console.WriteLine("\nFrequency Count:");
            foreach (var kvp in fruitFrequency)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            // Test case 2: Numbers
            Console.WriteLine("\n=== Test Case 2: Number Frequency ===");
            List<string> numbers = new List<string> { "1", "2", "3", "2", "1", "1", "4", "4", "4" };
            Console.WriteLine($"Input: [{string.Join(", ", numbers)}]");
            
            Dictionary<string, int> numberFrequency = CountFrequency(numbers);
            Console.WriteLine("\nFrequency Count:");
            foreach (var kvp in numberFrequency)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            // Test case 3: Colors
            Console.WriteLine("\n=== Test Case 3: Color Frequency ===");
            List<string> colors = new List<string> { "red", "blue", "red", "green", "blue", "red" };
            Console.WriteLine($"Input: [{string.Join(", ", colors)}]");
            
            Dictionary<string, int> colorFrequency = CountFrequency(colors);
            Console.WriteLine("\nFrequency Count (Sorted by frequency):");
            var sortedFrequency = SortByFrequency(colorFrequency);
            foreach (var kvp in sortedFrequency)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine("\n✓ Frequency counting completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static Dictionary<string, int> CountFrequency(List<string> elements)
    {
        Dictionary<string, int> frequency = new Dictionary<string, int>();

        foreach (var element in elements)
        {
            if (frequency.ContainsKey(element))
            {
                frequency[element]++;
            }
            else
            {
                frequency[element] = 1;
            }
        }

        return frequency;
    }

    static List<KeyValuePair<string, int>> SortByFrequency(Dictionary<string, int> frequency)
    {
        List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(frequency);
        list.Sort((a, b) => b.Value.CompareTo(a.Value)); // Sort in descending order
        return list;
    }
}
