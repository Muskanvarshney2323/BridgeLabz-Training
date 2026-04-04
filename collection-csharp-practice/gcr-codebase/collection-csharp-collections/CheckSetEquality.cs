using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 1: Check if Two Sets Are Equal
/// Compare two sets and determine if they contain the same elements, regardless of order.
/// Example:
/// Set1: {1, 2, 3}, Set2: {3, 2, 1}
/// Output: true
/// </summary>
class CheckSetEqualityProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║         Check if Two Sets Are Equal               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Equal sets
            Console.WriteLine("=== Test Case 1: Equal Sets ===");
            HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set2 = new HashSet<int> { 3, 2, 1 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set1.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set2.OrderBy(x => x))}}}");
            Console.WriteLine($"Are Equal: {AreEqual(set1, set2)}\n");

            // Test case 2: Different sets
            Console.WriteLine("=== Test Case 2: Different Sets ===");
            HashSet<int> set3 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set4 = new HashSet<int> { 1, 2, 4 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set3.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set4.OrderBy(x => x))}}}");
            Console.WriteLine($"Are Equal: {AreEqual(set3, set4)}\n");

            // Test case 3: Different sizes
            Console.WriteLine("=== Test Case 3: Different Sizes ===");
            HashSet<int> set5 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set6 = new HashSet<int> { 1, 2, 3, 4 };
            Console.WriteLine($"Set1: {{{string.Join(", ", set5.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set6.OrderBy(x => x))}}}");
            Console.WriteLine($"Are Equal: {AreEqual(set5, set6)}\n");

            // Test case 4: Empty sets
            Console.WriteLine("=== Test Case 4: Both Empty ===");
            HashSet<int> set7 = new HashSet<int>();
            HashSet<int> set8 = new HashSet<int>();
            Console.WriteLine($"Set1: {{}}");
            Console.WriteLine($"Set2: {{}}");
            Console.WriteLine($"Are Equal: {AreEqual(set7, set8)}\n");

            // Test case 5: String sets
            Console.WriteLine("=== Test Case 5: String Sets ===");
            HashSet<string> set9 = new HashSet<string> { "apple", "banana", "cherry" };
            HashSet<string> set10 = new HashSet<string> { "cherry", "apple", "banana" };
            Console.WriteLine($"Set1: {{{string.Join(", ", set9.OrderBy(x => x))}}}");
            Console.WriteLine($"Set2: {{{string.Join(", ", set10.OrderBy(x => x))}}}");
            Console.WriteLine($"Are Equal: {AreEqual(set9, set10)}\n");

            Console.WriteLine("✓ Set equality check completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static bool AreEqual<T>(HashSet<T> set1, HashSet<T> set2)
    {
        if (set1.Count != set2.Count)
            return false;

        return set1.SetEquals(set2);
    }
}
