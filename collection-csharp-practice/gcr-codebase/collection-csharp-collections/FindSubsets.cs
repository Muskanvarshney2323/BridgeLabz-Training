using System;
using System.Collections.Generic;

/// <summary>
/// Problem 5: Find Subsets
/// Check if one set is a subset of another.
/// Example:
/// Input: {2, 3}, {1, 2, 3, 4}
/// Output: true
/// </summary>
class FindSubsetsProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║              Find Subsets                         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Subset relationship
            Console.WriteLine("=== Test Case 1: Subset Relationship ===");
            HashSet<int> set1 = new HashSet<int> { 2, 3 };
            HashSet<int> set2 = new HashSet<int> { 1, 2, 3, 4 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set1)}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set2)}}}");
            Console.WriteLine($"Is Set1 subset of Set2: {IsSubset(set1, set2)}\n");

            // Test case 2: Not a subset
            Console.WriteLine("=== Test Case 2: Not a Subset ===");
            HashSet<int> set3 = new HashSet<int> { 2, 5 };
            HashSet<int> set4 = new HashSet<int> { 1, 2, 3, 4 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set3)}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set4)}}}");
            Console.WriteLine($"Is Set1 subset of Set2: {IsSubset(set3, set4)}\n");

            // Test case 3: Empty set is subset of any set
            Console.WriteLine("=== Test Case 3: Empty Set (Subset of Any) ===");
            HashSet<int> set5 = new HashSet<int>();
            HashSet<int> set6 = new HashSet<int> { 1, 2, 3 };
            Console.WriteLine($"Set1 (Empty): {{}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set6)}}}");
            Console.WriteLine($"Is Set1 subset of Set2: {IsSubset(set5, set6)}\n");

            // Test case 4: Set equal to parent set
            Console.WriteLine("=== Test Case 4: Set Equal to Parent ===");
            HashSet<int> set7 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set8 = new HashSet<int> { 1, 2, 3 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set7)}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set8)}}}");
            Console.WriteLine($"Is Set1 subset of Set2: {IsSubset(set7, set8)}\n");

            // Test case 5: String sets
            Console.WriteLine("=== Test Case 5: String Sets ===");
            HashSet<string> set9 = new HashSet<string> { "apple", "banana" };
            HashSet<string> set10 = new HashSet<string> { "apple", "banana", "cherry", "date" };
            Console.WriteLine($"Set1: {{{string.Join(", ", set9)}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set10)}}}");
            Console.WriteLine($"Is Set1 subset of Set2: {IsSubset(set9, set10)}\n");

            // Test case 6: Proper subset vs improper subset
            Console.WriteLine("=== Test Case 6: Proper vs Improper Subset ===");
            HashSet<int> set11 = new HashSet<int> { 1, 2 };
            HashSet<int> set12 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set13 = new HashSet<int> { 1, 2 };
            
            Console.WriteLine($"Set1: {{{string.Join(", ", set11)}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set12)}}}");
            Console.WriteLine($"Set3: {{{string.Join(", ", set13)}}}");
            Console.WriteLine($"Is Set1 subset of Set2 (Proper): {IsSubset(set11, set12)}");
            Console.WriteLine($"Is Set1 subset of Set3 (Improper): {IsSubset(set11, set13)}");
            Console.WriteLine($"Is Set1 proper subset of Set2: {IsProperSubset(set11, set12)}");
            Console.WriteLine($"Is Set1 proper subset of Set3: {IsProperSubset(set11, set13)}\n");

            Console.WriteLine("✓ Subset check completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static bool IsSubset<T>(HashSet<T> subSet, HashSet<T> superSet)
    {
        return subSet.IsSubsetOf(superSet);
    }

    static bool IsProperSubset<T>(HashSet<T> subSet, HashSet<T> superSet)
    {
        return subSet.IsProperSubsetOf(superSet);
    }
}
