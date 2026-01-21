using System;
using System.Collections.Generic;

/// <summary>
/// Problem 1: Reverse a Queue
/// Reverse the elements of a queue using only queue operations.
/// Example:
/// Input: [10, 20, 30]
/// Output: [30, 20, 10]
/// </summary>
class ReverseQueueProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║            Reverse a Queue                        ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Integer queue
            Console.WriteLine("=== Test Case 1: Integer Queue ===");
            Queue<int> queue1 = new Queue<int>(new[] { 10, 20, 30 });
            Console.WriteLine($"Original Queue: {QueueToString(queue1)}");
            
            Queue<int> reversedQueue1 = ReverseQueue(queue1);
            Console.WriteLine($"Reversed Queue: {QueueToString(reversedQueue1)}\n");

            // Test case 2: String queue
            Console.WriteLine("=== Test Case 2: String Queue ===");
            Queue<string> queue2 = new Queue<string>(new[] { "A", "B", "C", "D" });
            Console.WriteLine($"Original Queue: {QueueToString(queue2)}");
            
            Queue<string> reversedQueue2 = ReverseQueue(queue2);
            Console.WriteLine($"Reversed Queue: {QueueToString(reversedQueue2)}\n");

            // Test case 3: Single element
            Console.WriteLine("=== Test Case 3: Single Element Queue ===");
            Queue<int> queue3 = new Queue<int>(new[] { 42 });
            Console.WriteLine($"Original Queue: {QueueToString(queue3)}");
            
            Queue<int> reversedQueue3 = ReverseQueue(queue3);
            Console.WriteLine($"Reversed Queue: {QueueToString(reversedQueue3)}\n");

            // Test case 4: Empty queue
            Console.WriteLine("=== Test Case 4: Empty Queue ===");
            Queue<int> queue4 = new Queue<int>();
            Console.WriteLine($"Original Queue: Empty");
            
            Queue<int> reversedQueue4 = ReverseQueue(queue4);
            Console.WriteLine($"Reversed Queue: {QueueToString(reversedQueue4)}\n");

            // Test case 5: Large queue
            Console.WriteLine("=== Test Case 5: Large Queue ===");
            Queue<int> queue5 = new Queue<int>();
            for (int i = 1; i <= 5; i++)
            {
                queue5.Enqueue(i * 10);
            }
            Console.WriteLine($"Original Queue: {QueueToString(queue5)}");
            
            Queue<int> reversedQueue5 = ReverseQueue(queue5);
            Console.WriteLine($"Reversed Queue: {QueueToString(reversedQueue5)}\n");

            Console.WriteLine("✓ Queue reversal completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static Queue<T> ReverseQueue<T>(Queue<T> queue)
    {
        Stack<T> stack = new Stack<T>();
        
        // Transfer queue to stack (reverses order)
        while (queue.Count > 0)
        {
            stack.Push(queue.Dequeue());
        }
        
        // Transfer stack back to queue
        Queue<T> reversedQueue = new Queue<T>();
        while (stack.Count > 0)
        {
            reversedQueue.Enqueue(stack.Pop());
        }
        
        return reversedQueue;
    }

    static string QueueToString<T>(Queue<T> queue)
    {
        if (queue.Count == 0)
            return "[]";
        
        List<T> elements = new List<T>(queue);
        return "[" + string.Join(", ", elements) + "]";
    }
}
