using System;
using System.Collections.Generic;

/// <summary>
/// Problem 5: Find the Nth Element from the End
/// Given a singly linked list, find the Nth element from the end without calculating its size.
/// Example:
/// Input: [A, B, C, D, E], N=2
/// Output: D
/// </summary>
class NthElementFromEndProgram
{
    class Node
    {
        public string Data;
        public Node Next;

        public Node(string data)
        {
            Data = data;
            Next = null;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║      Find the Nth Element from the End            ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Find 2nd element from end
            Console.WriteLine("=== Test Case 1: Find 2nd Element from End ===");
            Node head1 = CreateLinkedList(new[] { "A", "B", "C", "D", "E" });
            Console.WriteLine("Linked List: A -> B -> C -> D -> E");
            Console.WriteLine("Finding: 2nd element from end");
            
            string result1 = FindNthFromEnd(head1, 2);
            Console.WriteLine($"Result: {result1}\n");

            // Test case 2: Find 1st element from end
            Console.WriteLine("=== Test Case 2: Find 1st Element from End ===");
            Node head2 = CreateLinkedList(new[] { "X", "Y", "Z" });
            Console.WriteLine("Linked List: X -> Y -> Z");
            Console.WriteLine("Finding: 1st element from end");
            
            string result2 = FindNthFromEnd(head2, 1);
            Console.WriteLine($"Result: {result2}\n");

            // Test case 3: Find Nth where N = list length
            Console.WriteLine("=== Test Case 3: Find Nth Element (N = Length) ===");
            Node head3 = CreateLinkedList(new[] { "1", "2", "3", "4", "5" });
            Console.WriteLine("Linked List: 1 -> 2 -> 3 -> 4 -> 5");
            Console.WriteLine("Finding: 5th element from end");
            
            string result3 = FindNthFromEnd(head3, 5);
            Console.WriteLine($"Result: {result3}\n");

            // Test case 4: Single element list
            Console.WriteLine("=== Test Case 4: Single Element List ===");
            Node head4 = CreateLinkedList(new[] { "ONLY" });
            Console.WriteLine("Linked List: ONLY");
            Console.WriteLine("Finding: 1st element from end");
            
            string result4 = FindNthFromEnd(head4, 1);
            Console.WriteLine($"Result: {result4}\n");

            // Test case 5: Large list
            Console.WriteLine("=== Test Case 5: Find 3rd Element from End (Large List) ===");
            Node head5 = CreateLinkedList(new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" });
            Console.WriteLine("Linked List: A -> B -> C -> D -> E -> F -> G -> H -> I -> J");
            Console.WriteLine("Finding: 3rd element from end");
            
            string result5 = FindNthFromEnd(head5, 3);
            Console.WriteLine($"Result: {result5}\n");

            Console.WriteLine("✓ Finding Nth element completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static Node CreateLinkedList(string[] values)
    {
        if (values.Length == 0) return null;
        
        Node head = new Node(values[0]);
        Node current = head;

        for (int i = 1; i < values.Length; i++)
        {
            current.Next = new Node(values[i]);
            current = current.Next;
        }

        return head;
    }

    static string FindNthFromEnd(Node head, int n)
    {
        if (head == null) return "List is empty";

        // Use two pointers approach
        Node first = head;
        Node second = head;

        // Move first pointer n steps ahead
        for (int i = 0; i < n; i++)
        {
            if (first == null)
            {
                return "N is greater than list length";
            }
            first = first.Next;
        }

        // Move both pointers until first reaches null
        while (first != null)
        {
            first = first.Next;
            second = second.Next;
        }

        return second.Data;
    }
}
