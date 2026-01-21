using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 4: Convert a Set to a Sorted List
/// Convert a HashSet<int> into a sorted list in ascending order.
/// Example:
/// Input: {5, 3, 9, 1}
/// Output: [1, 3, 5, 9]
/// </summary>
class SetToSortedListProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║       Convert a Set to a Sorted List              ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Integer set to sorted list
            Console.WriteLine("=== Test Case 1: Integer Set to Sorted List ===");
            HashSet<int> set1 = new HashSet<int> { 5, 3, 9, 1, 7, 2 };
            Console.WriteLine($"Original Set: {{{string.Join(", ", set1)}}}");
            
            List<int> sortedList1 = ConvertToSortedList(set1);
            Console.WriteLine($"Sorted List (Ascending): [{string.Join(", ", sortedList1)}]\n");

            // Test case 2: String set to sorted list
            Console.WriteLine("=== Test Case 2: String Set to Sorted List ===");
            HashSet<string> set2 = new HashSet<string> { "zebra", "apple", "mango", "banana" };
            Console.WriteLine($"Original Set: {{{string.Join(", ", set2)}}}");
            
            List<string> sortedList2 = ConvertToSortedList(set2);
            Console.WriteLine($"Sorted List (Ascending): [{string.Join(", ", sortedList2)}]\n");

            // Test case 3: Descending order
            Console.WriteLine("=== Test Case 3: Integer Set to Sorted List (Descending) ===");
            HashSet<int> set3 = new HashSet<int> { 10, 25, 5, 15, 30 };
            Console.WriteLine($"Original Set: {{{string.Join(", ", set3)}}}");
            
            List<int> sortedList3 = ConvertToSortedListDescending(set3);
            Console.WriteLine($"Sorted List (Descending): [{string.Join(", ", sortedList3)}]\n");

            // Test case 4: Duplicate removal while sorting
            Console.WriteLine("=== Test Case 4: Duplicate Removal (Already in Set) ===");
            HashSet<int> set4 = new HashSet<int> { 4, 2, 8, 1, 2, 4, 6 }; // Set already removed duplicates
            Console.WriteLine($"Original Set: {{{string.Join(", ", set4)}}}");
            Console.WriteLine($"Count (no duplicates): {set4.Count}");
            
            List<int> sortedList4 = ConvertToSortedList(set4);
            Console.WriteLine($"Sorted List: [{string.Join(", ", sortedList4)}]\n");

            // Test case 5: Empty set
            Console.WriteLine("=== Test Case 5: Empty Set ===");
            HashSet<int> set5 = new HashSet<int>();
            Console.WriteLine($"Original Set: {{}}");
            
            List<int> sortedList5 = ConvertToSortedList(set5);
            Console.WriteLine($"Sorted List: [{string.Join(", ", sortedList5)}]\n");

            // Test case 6: Single element
            Console.WriteLine("=== Test Case 6: Single Element Set ===");
            HashSet<int> set6 = new HashSet<int> { 42 };
            Console.WriteLine($"Original Set: {{{string.Join(", ", set6)}}}");
            
            List<int> sortedList6 = ConvertToSortedList(set6);
            Console.WriteLine($"Sorted List: [{string.Join(", ", sortedList6)}]\n");

            Console.WriteLine("✓ Set to sorted list conversion completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static List<T> ConvertToSortedList<T>(HashSet<T> set) where T : IComparable<T>
    {
        List<T> list = new List<T>(set);
        list.Sort();
        return list;
    }

    static List<T> ConvertToSortedListDescending<T>(HashSet<T> set) where T : IComparable<T>
    {
        List<T> list = new List<T>(set);
        list.Sort((a, b) => b.CompareTo(a));
        return list;
    }
}
