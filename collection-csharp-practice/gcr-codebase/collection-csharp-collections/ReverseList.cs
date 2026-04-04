using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Problem 1: Reverse a List
/// Reverses the elements of a given list without using built-in reverse methods.
/// Implements for both ArrayList and LinkedList.
/// Example:
/// Input: [1, 2, 3, 4, 5]
/// Output: [5, 4, 3, 2, 1]
/// </summary>
class ReverseListProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║           Reverse a List                          ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test with ArrayList
            Console.WriteLine("=== Reversing ArrayList ===");
            ArrayList arrayList = new ArrayList { 1, 2, 3, 4, 5 };
            Console.WriteLine($"Original ArrayList: [{string.Join(", ", arrayList.ToArray())}]");
            
            ArrayList reversedArrayList = ReverseArrayList(arrayList);
            Console.WriteLine($"Reversed ArrayList: [{string.Join(", ", reversedArrayList.ToArray())}]\n");

            // Test with LinkedList<int>
            Console.WriteLine("=== Reversing LinkedList<int> ===");
            LinkedList<int> linkedList = new LinkedList<int>(new[] { 10, 20, 30, 40, 50 });
            Console.WriteLine($"Original LinkedList: [{string.Join(", ", linkedList)}]");
            
            LinkedList<int> reversedLinkedList = ReverseLinkedList(linkedList);
            Console.WriteLine($"Reversed LinkedList: [{string.Join(", ", reversedLinkedList)}]\n");

            // Test with List<string>
            Console.WriteLine("=== Reversing List<string> ===");
            List<string> stringList = new List<string> { "apple", "banana", "cherry", "date" };
            Console.WriteLine($"Original List: [{string.Join(", ", stringList)}]");
            
            List<string> reversedStringList = ReverseList(stringList);
            Console.WriteLine($"Reversed List: [{string.Join(", ", reversedStringList)}]\n");

            Console.WriteLine("✓ Reversal completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static ArrayList ReverseArrayList(ArrayList list)
    {
        ArrayList reversed = new ArrayList();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            reversed.Add(list[i]);
        }
        return reversed;
    }

    static LinkedList<int> ReverseLinkedList(LinkedList<int> list)
    {
        LinkedList<int> reversed = new LinkedList<int>();
        LinkedListNode<int> current = list.Last;
        while (current != null)
        {
            reversed.AddLast(current.Value);
            current = current.Previous;
        }
        return reversed;
    }

    static List<T> ReverseList<T>(List<T> list)
    {
        List<T> reversed = new List<T>();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            reversed.Add(list[i]);
        }
        return reversed;
    }
}
