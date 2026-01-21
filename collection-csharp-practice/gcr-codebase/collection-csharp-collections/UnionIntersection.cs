using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 2: Union and Intersection of Two Sets
/// Compute the union and intersection of two sets.
/// Example:
/// Set1: {1, 2, 3}, Set2: {3, 4, 5}
/// Output:
/// Union: {1, 2, 3, 4, 5}
/// Intersection: {3}
/// </summary>
class UnionIntersectionProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║    Union and Intersection of Two Sets             ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Integer sets
            Console.WriteLine("=== Test Case 1: Integer Sets ===");
            HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set2 = new HashSet<int> { 3, 4, 5 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set1.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set2.OrderBy(x => x))}}}");
            
            var union1 = GetUnion(set1, set2);
            var intersection1 = GetIntersection(set1, set2);
            Console.WriteLine($"Union: {{{string.Join(", ", union1.OrderBy(x => x))}}}");
            Console.WriteLine($"Intersection: {{{string.Join(", ", intersection1.OrderBy(x => x))}}}\n");

            // Test case 2: String sets
            Console.WriteLine("=== Test Case 2: String Sets ===");
            HashSet<string> set3 = new HashSet<string> { "apple", "banana", "cherry" };
            HashSet<string> set4 = new HashSet<string> { "cherry", "date", "elderberry" };
            Console.WriteLine($"Set1: {{{string.Join(", ", set3.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set4.OrderBy(x => x))}}}");
            
            var union2 = GetUnion(set3, set4);
            var intersection2 = GetIntersection(set3, set4);
            Console.WriteLine($"Union: {{{string.Join(", ", union2.OrderBy(x => x))}}}");
            Console.WriteLine($"Intersection: {{{string.Join(", ", intersection2.OrderBy(x => x))}}}\n");

            // Test case 3: No intersection
            Console.WriteLine("=== Test Case 3: No Intersection ===");
            HashSet<int> set5 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set6 = new HashSet<int> { 4, 5, 6 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set5.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set6.OrderBy(x => x))}}}");
            
            var union3 = GetUnion(set5, set6);
            var intersection3 = GetIntersection(set5, set6);
            Console.WriteLine($"Union: {{{string.Join(", ", union3.OrderBy(x => x))}}}");
            Console.WriteLine($"Intersection: {{{string.Join(", ", intersection3.OrderBy(x => x))}}}\n");

            // Test case 4: Complete overlap
            Console.WriteLine("=== Test Case 4: Complete Overlap ===");
            HashSet<int> set7 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set8 = new HashSet<int> { 1, 2, 3 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set7.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set8.OrderBy(x => x))}}}");
            
            var union4 = GetUnion(set7, set8);
            var intersection4 = GetIntersection(set7, set8);
            Console.WriteLine($"Union: {{{string.Join(", ", union4.OrderBy(x => x))}}}");
            Console.WriteLine($"Intersection: {{{string.Join(", ", intersection4.OrderBy(x => x))}}}\n");

            Console.WriteLine("✓ Union and Intersection operations completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static HashSet<T> GetUnion<T>(HashSet<T> set1, HashSet<T> set2)
    {
        HashSet<T> union = new HashSet<T>(set1);
        union.UnionWith(set2);
        return union;
    }

    static HashSet<T> GetIntersection<T>(HashSet<T> set1, HashSet<T> set2)
    {
        HashSet<T> intersection = new HashSet<T>(set1);
        intersection.IntersectWith(set2);
        return intersection;
    }
}
