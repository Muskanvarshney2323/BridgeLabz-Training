using System;
using System.Collections.Generic;

/// <summary>
/// Problem 2: Generate Binary Numbers Using a Queue
/// Generate the first N binary numbers using a queue.
/// Example:
/// Input: N=5
/// Output: {"1", "10", "11", "100", "101"}
/// </summary>
class BinaryNumbersQueueProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Generate Binary Numbers Using a Queue           ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: First 5 binary numbers
            Console.WriteLine("=== Test Case 1: First 5 Binary Numbers ===");
            List<string> binary1 = GenerateBinaryNumbers(5);
            Console.WriteLine("Output: {" + string.Join(", ", binary1) + "}\n");

            // Test case 2: First 10 binary numbers
            Console.WriteLine("=== Test Case 2: First 10 Binary Numbers ===");
            List<string> binary2 = GenerateBinaryNumbers(10);
            Console.WriteLine("Output: {" + string.Join(", ", binary2) + "}\n");

            // Test case 3: First 1 binary number
            Console.WriteLine("=== Test Case 3: First 1 Binary Number ===");
            List<string> binary3 = GenerateBinaryNumbers(1);
            Console.WriteLine("Output: {" + string.Join(", ", binary3) + "}\n");

            // Test case 4: First 8 binary numbers
            Console.WriteLine("=== Test Case 4: First 8 Binary Numbers ===");
            List<string> binary4 = GenerateBinaryNumbers(8);
            Console.WriteLine("Output: {" + string.Join(", ", binary4) + "}\n");

            // Test case 5: First 16 binary numbers
            Console.WriteLine("=== Test Case 5: First 16 Binary Numbers ===");
            List<string> binary5 = GenerateBinaryNumbers(16);
            Console.WriteLine("Output: {" + string.Join(", ", binary5) + "}\n");

            Console.WriteLine("✓ Binary number generation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static List<string> GenerateBinaryNumbers(int n)
    {
        List<string> result = new List<string>();
        
        if (n <= 0)
            return result;

        Queue<string> queue = new Queue<string>();
        queue.Enqueue("1");

        for (int i = 0; i < n; i++)
        {
            string current = queue.Dequeue();
            result.Add(current);

            // Generate next binary numbers by appending 0 and 1
            queue.Enqueue(current + "0");
            queue.Enqueue(current + "1");
        }

        return result;
    }
}
