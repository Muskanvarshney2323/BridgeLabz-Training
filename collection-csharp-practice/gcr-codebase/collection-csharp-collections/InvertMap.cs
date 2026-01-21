using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 2: Invert a Map
/// Invert a Dictionary<K, V> to produce a Dictionary<V, List<K>>.
/// Example:
/// Input: { A=1, B=2, C=1 }
/// Output: { 1=[A, C], 2=[B] }
/// </summary>
class InvertMapProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║              Invert a Map                         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: String to integer map
            Console.WriteLine("=== Test Case 1: String to Integer Map ===");
            Dictionary<string, int> map1 = new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 },
                { "C", 1 },
                { "D", 3 },
                { "E", 2 }
            };
            
            Console.WriteLine("Original Map: { A=1, B=2, C=1, D=3, E=2 }");
            Console.WriteLine("Inverted Map:");
            Dictionary<int, List<string>> inverted1 = InvertMap(map1);
            DisplayInvertedMap(inverted1);
            Console.WriteLine();

            // Test case 2: Integer to string map
            Console.WriteLine("=== Test Case 2: Integer to String Map ===");
            Dictionary<int, string> map2 = new Dictionary<int, string>
            {
                { 1, "One" },
                { 2, "Two" },
                { 3, "One" },
                { 4, "Three" },
                { 5, "Two" }
            };
            
            Console.WriteLine("Original Map: { 1=One, 2=Two, 3=One, 4=Three, 5=Two }");
            Console.WriteLine("Inverted Map:");
            Dictionary<string, List<int>> inverted2 = InvertMap(map2);
            DisplayInvertedMap(inverted2);
            Console.WriteLine();

            // Test case 3: All unique values
            Console.WriteLine("=== Test Case 3: All Unique Values ===");
            Dictionary<string, int> map3 = new Dictionary<string, int>
            {
                { "X", 10 },
                { "Y", 20 },
                { "Z", 30 }
            };
            
            Console.WriteLine("Original Map: { X=10, Y=20, Z=30 }");
            Console.WriteLine("Inverted Map:");
            Dictionary<int, List<string>> inverted3 = InvertMap(map3);
            DisplayInvertedMap(inverted3);
            Console.WriteLine();

            // Test case 4: All same value
            Console.WriteLine("=== Test Case 4: All Same Value ===");
            Dictionary<string, string> map4 = new Dictionary<string, string>
            {
                { "Key1", "Same" },
                { "Key2", "Same" },
                { "Key3", "Same" },
                { "Key4", "Same" }
            };
            
            Console.WriteLine("Original Map: { Key1=Same, Key2=Same, Key3=Same, Key4=Same }");
            Console.WriteLine("Inverted Map:");
            Dictionary<string, List<string>> inverted4 = InvertMap(map4);
            DisplayInvertedMap(inverted4);
            Console.WriteLine();

            // Test case 5: Empty map
            Console.WriteLine("=== Test Case 5: Empty Map ===");
            Dictionary<string, int> map5 = new Dictionary<string, int>();
            
            Console.WriteLine("Original Map: {}");
            Console.WriteLine("Inverted Map:");
            Dictionary<int, List<string>> inverted5 = InvertMap(map5);
            if (inverted5.Count == 0)
            {
                Console.WriteLine("  {}");
            }
            else
            {
                DisplayInvertedMap(inverted5);
            }
            Console.WriteLine();

            // Test case 6: Student to grade mapping
            Console.WriteLine("=== Test Case 6: Student to Grade Mapping ===");
            Dictionary<string, char> map6 = new Dictionary<string, char>
            {
                { "Alice", 'A' },
                { "Bob", 'B' },
                { "Charlie", 'A' },
                { "David", 'C' },
                { "Eve", 'B' }
            };
            
            Console.WriteLine("Original Map: { Alice=A, Bob=B, Charlie=A, David=C, Eve=B }");
            Console.WriteLine("Inverted Map (by Grade):");
            Dictionary<char, List<string>> inverted6 = InvertMap(map6);
            DisplayInvertedMap(inverted6);
            Console.WriteLine();

            Console.WriteLine("✓ Map inversion completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static Dictionary<V, List<K>> InvertMap<K, V>(Dictionary<K, V> originalMap) where K : notnull where V : notnull
    {
        Dictionary<V, List<K>> invertedMap = new Dictionary<V, List<K>>();

        foreach (var kvp in originalMap)
        {
            if (!invertedMap.ContainsKey(kvp.Value))
            {
                invertedMap[kvp.Value] = new List<K>();
            }

            invertedMap[kvp.Value].Add(kvp.Key);
        }

        return invertedMap;
    }

    static void DisplayInvertedMap<K, V>(Dictionary<K, List<V>> map) where K : notnull
    {
        if (map.Count == 0)
        {
            Console.WriteLine("  {}");
            return;
        }

        foreach (var kvp in map.OrderBy(x => x.Key))
        {
            string valueList = "[" + string.Join(", ", kvp.Value.OrderBy(x => x)) + "]";
            Console.WriteLine($"  {kvp.Key}={valueList}");
        }
    }
}
