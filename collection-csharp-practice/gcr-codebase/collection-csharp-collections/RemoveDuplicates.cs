using System;
using System.Collections.Generic;

/// <summary>
/// Problem 4: Remove Duplicates While Preserving Order
/// Remove duplicate elements from a list while maintaining the original order.
/// Example:
/// Input: [3, 1, 2, 2, 3, 4]
/// Output: [3, 1, 2, 4]
/// </summary>
class RemoveDuplicatesProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Remove Duplicates While Preserving Order        ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Integers
            Console.WriteLine("=== Test Case 1: Remove Duplicates from Integer List ===");
            List<int> list1 = new List<int> { 3, 1, 2, 2, 3, 4, 1, 5 };
            Console.WriteLine($"Original List: [{string.Join(", ", list1)}]");
            
            List<int> noDupsInt = RemoveDuplicates(list1);
            Console.WriteLine($"After Removing Duplicates: [{string.Join(", ", noDupsInt)}]\n");

            // Test case 2: Strings
            Console.WriteLine("=== Test Case 2: Remove Duplicates from String List ===");
            List<string> list2 = new List<string> { "apple", "banana", "apple", "orange", "banana", "grape" };
            Console.WriteLine($"Original List: [{string.Join(", ", list2)}]");
            
            List<string> noDupsString = RemoveDuplicates(list2);
            Console.WriteLine($"After Removing Duplicates: [{string.Join(", ", noDupsString)}]\n");

            // Test case 3: Characters
            Console.WriteLine("=== Test Case 3: Remove Duplicates from Character List ===");
            List<char> list3 = new List<char> { 'a', 'b', 'c', 'a', 'b', 'd', 'e', 'c' };
            Console.WriteLine($"Original List: [{string.Join(", ", list3)}]");
            
            List<char> noDupsChar = RemoveDuplicates(list3);
            Console.WriteLine($"After Removing Duplicates: [{string.Join(", ", noDupsChar)}]\n");

            // Test case 4: All duplicates
            Console.WriteLine("=== Test Case 4: All Same Elements ===");
            List<int> list4 = new List<int> { 5, 5, 5, 5, 5 };
            Console.WriteLine($"Original List: [{string.Join(", ", list4)}]");
            
            List<int> noDupsAllSame = RemoveDuplicates(list4);
            Console.WriteLine($"After Removing Duplicates: [{string.Join(", ", noDupsAllSame)}]\n");

            // Test case 5: No duplicates
            Console.WriteLine("=== Test Case 5: No Duplicates ===");
            List<int> list5 = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine($"Original List: [{string.Join(", ", list5)}]");
            
            List<int> noDupsNone = RemoveDuplicates(list5);
            Console.WriteLine($"After Removing Duplicates: [{string.Join(", ", noDupsNone)}]\n");

            Console.WriteLine("✓ Duplicate removal completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static List<T> RemoveDuplicates<T>(List<T> list)
    {
        HashSet<T> seen = new HashSet<T>();
        List<T> result = new List<T>();

        foreach (var element in list)
        {
            if (!seen.Contains(element))
            {
                seen.Add(element);
                result.Add(element);
            }
        }

        return result;
    }
}
