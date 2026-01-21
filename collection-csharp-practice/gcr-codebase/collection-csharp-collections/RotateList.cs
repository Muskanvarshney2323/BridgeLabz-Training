using System;
using System.Collections.Generic;

/// <summary>
/// Problem 3: Rotate Elements in a List
/// Rotate the elements of a list by a given number of positions.
/// Example:
/// Input: [10, 20, 30, 40, 50], rotate by 2
/// Output: [30, 40, 50, 10, 20]
/// </summary>
class RotateListProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║        Rotate Elements in a List                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Rotate right
            Console.WriteLine("=== Test Case 1: Rotate Right by 2 ===");
            List<int> list1 = new List<int> { 10, 20, 30, 40, 50 };
            Console.WriteLine($"Original List: [{string.Join(", ", list1)}]");
            Console.WriteLine("Rotate right by: 2");
            
            List<int> rotatedRight = RotateRight(list1, 2);
            Console.WriteLine($"Rotated List: [{string.Join(", ", rotatedRight)}]\n");

            // Test case 2: Rotate left
            Console.WriteLine("=== Test Case 2: Rotate Left by 3 ===");
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5, 6 };
            Console.WriteLine($"Original List: [{string.Join(", ", list2)}]");
            Console.WriteLine("Rotate left by: 3");
            
            List<int> rotatedLeft = RotateLeft(list2, 3);
            Console.WriteLine($"Rotated List: [{string.Join(", ", rotatedLeft)}]\n");

            // Test case 3: Rotate with large k
            Console.WriteLine("=== Test Case 3: Rotate Right by 7 (k > list size) ===");
            List<int> list3 = new List<int> { 1, 2, 3, 4, 5 };
            int k = 7; // k is greater than list size
            Console.WriteLine($"Original List: [{string.Join(", ", list3)}]");
            Console.WriteLine($"Rotate right by: {k}");
            
            List<int> rotatedLarge = RotateRight(list3, k);
            Console.WriteLine($"Rotated List: [{string.Join(", ", rotatedLarge)}]\n");

            // Test case 4: Rotate with strings
            Console.WriteLine("=== Test Case 4: Rotate String List by 2 ===");
            List<string> list4 = new List<string> { "A", "B", "C", "D", "E" };
            Console.WriteLine($"Original List: [{string.Join(", ", list4)}]");
            Console.WriteLine("Rotate right by: 2");
            
            List<string> rotatedStrings = RotateRight(list4, 2);
            Console.WriteLine($"Rotated List: [{string.Join(", ", rotatedStrings)}]\n");

            Console.WriteLine("✓ Rotation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static List<T> RotateRight<T>(List<T> list, int k)
    {
        if (list.Count == 0) return list;

        k = k % list.Count; // Handle k > list.Count

        List<T> rotated = new List<T>();
        
        // Add last k elements
        for (int i = list.Count - k; i < list.Count; i++)
        {
            rotated.Add(list[i]);
        }
        
        // Add remaining elements
        for (int i = 0; i < list.Count - k; i++)
        {
            rotated.Add(list[i]);
        }

        return rotated;
    }

    static List<T> RotateLeft<T>(List<T> list, int k)
    {
        if (list.Count == 0) return list;

        k = k % list.Count; // Handle k > list.Count

        List<T> rotated = new List<T>();
        
        // Add elements starting from k
        for (int i = k; i < list.Count; i++)
        {
            rotated.Add(list[i]);
        }
        
        // Add first k elements
        for (int i = 0; i < k; i++)
        {
            rotated.Add(list[i]);
        }

        return rotated;
    }
}
