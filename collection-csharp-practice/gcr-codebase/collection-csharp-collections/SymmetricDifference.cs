using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 3: Symmetric Difference
/// Find the symmetric difference (elements present in either set but not both).
/// Example:
/// Input: {1, 2, 3}, {3, 4, 5}
/// Output: {1, 2, 4, 5}
/// </summary>
class SymmetricDifferenceProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║            Symmetric Difference                   ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Integer sets
            Console.WriteLine("=== Test Case 1: Integer Sets ===");
            HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set2 = new HashSet<int> { 3, 4, 5 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set1.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set2.OrderBy(x => x))}}}");
            
            var symmetricDiff1 = GetSymmetricDifference(set1, set2);
            Console.WriteLine($"Symmetric Difference: {{{string.Join(", ", symmetricDiff1.OrderBy(x => x))}}}\n");

            // Test case 2: String sets
            Console.WriteLine("=== Test Case 2: String Sets ===");
            HashSet<string> set3 = new HashSet<string> { "apple", "banana", "cherry" };
            HashSet<string> set4 = new HashSet<string> { "cherry", "date", "elderberry" };
            Console.WriteLine($"Set1: {{{string.Join(", ", set3.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set4.OrderBy(x => x))}}}");
            
            var symmetricDiff2 = GetSymmetricDifference(set3, set4);
            Console.WriteLine($"Symmetric Difference: {{{string.Join(", ", symmetricDiff2.OrderBy(x => x))}}}\n");

            // Test case 3: No common elements
            Console.WriteLine("=== Test Case 3: No Common Elements ===");
            HashSet<int> set5 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set6 = new HashSet<int> { 4, 5, 6 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set5.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set6.OrderBy(x => x))}}}");
            
            var symmetricDiff3 = GetSymmetricDifference(set5, set6);
            Console.WriteLine($"Symmetric Difference: {{{string.Join(", ", symmetricDiff3.OrderBy(x => x))}}}\n");

            // Test case 4: All common elements
            Console.WriteLine("=== Test Case 4: All Common Elements ===");
            HashSet<int> set7 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set8 = new HashSet<int> { 1, 2, 3 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set7.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set8.OrderBy(x => x))}}}");
            
            var symmetricDiff4 = GetSymmetricDifference(set7, set8);
            Console.WriteLine($"Symmetric Difference: {{{string.Join(", ", symmetricDiff4.OrderBy(x => x))}}}\n");

            // Test case 5: Complex case
            Console.WriteLine("=== Test Case 5: Complex Case ===");
            HashSet<int> set9 = new HashSet<int> { 1, 2, 3, 4, 5 };
            HashSet<int> set10 = new HashSet<int> { 3, 4, 5, 6, 7, 8 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set9.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set10.OrderBy(x => x))}}}");
            
            var symmetricDiff5 = GetSymmetricDifference(set9, set10);
            Console.WriteLine($"Symmetric Difference: {{{string.Join(", ", symmetricDiff5.OrderBy(x => x))}}}\n");

            Console.WriteLine("✓ Symmetric difference calculation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static HashSet<T> GetSymmetricDifference<T>(HashSet<T> set1, HashSet<T> set2)
    {
        // Method 1: Using SymmetricExceptWith
        HashSet<T> result = new HashSet<T>(set1);
        result.SymmetricExceptWith(set2);
        return result;

        // Alternative Method 2: Union - Intersection
        // HashSet<T> union = new HashSet<T>(set1);
        // union.UnionWith(set2);
        // 
        // HashSet<T> intersection = new HashSet<T>(set1);
        // intersection.IntersectWith(set2);
        // 
        // HashSet<T> result = new HashSet<T>(union);
        // result.ExceptWith(intersection);
        // return result;
    }
}
