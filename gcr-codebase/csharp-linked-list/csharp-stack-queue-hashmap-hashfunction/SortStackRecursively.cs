using System;
using System.Collections.Generic;

namespace StackQueueProblems
{
    /// <summary>
    /// Problem 2: Sort a Stack Using Recursion
    /// 
    /// Given a stack, sort its elements in ascending order using recursion.
    /// 
    /// Hint: Pop elements recursively, sort the remaining stack, 
    /// and insert the popped element back at the correct position.
    /// 
    /// Time Complexity: O(n^2) where n is the number of elements
    /// Space Complexity: O(n) for recursion stack
    /// </summary>
    public class SortStackRecursively
    {
        /// <summary>
        /// Sorts a stack in ascending order (smallest at the top)
        /// </summary>
        public static void SortStack(Stack<int> stack)
        {
            // Base case: if stack is empty, return
            if (stack.Count > 0)
            {
                // Pop the top element
                int top = stack.Pop();

                // Recursively sort the remaining stack
                SortStack(stack);

                // Insert the popped element at the correct position
                InsertSorted(stack, top);
            }
        }

        /// <summary>
        /// Helper method to insert an element at the correct position in a sorted stack
        /// Maintains the stack in ascending order with smallest element at top
        /// </summary>
        private static void InsertSorted(Stack<int> stack, int element)
        {
            // Base case: if stack is empty or top element is less than or equal to element
            if (stack.Count == 0 || stack.Peek() <= element)
            {
                stack.Push(element);
                return;
            }

            // Pop the top element
            int top = stack.Pop();

            // Recursively insert the element at the correct position
            InsertSorted(stack, element);

            // Push the popped element back
            stack.Push(top);
        }

        /// <summary>
        /// Print stack elements from top to bottom
        /// </summary>
        private static void PrintStack(Stack<int> stack)
        {
            Stack<int> temp = new Stack<int>(stack);
            Console.WriteLine("Stack (top to bottom):");
            while (temp.Count > 0)
            {
                Console.WriteLine(temp.Pop());
            }
        }
    }

    // Example Usage
    public class SortStackRecursivelyExample
    {
        public static void Main()
        {
            Stack<int> stack = new Stack<int>();

            // Create an unsorted stack
            stack.Push(34);
            stack.Push(3);
            stack.Push(31);
            stack.Push(98);
            stack.Push(92);
            stack.Push(23);

            Console.WriteLine("Original Stack:");
            PrintStack(stack);

            SortStackRecursively.SortStack(stack);

            Console.WriteLine("\nSorted Stack (ascending order, smallest at top):");
            PrintStack(stack);
        }

        private static void PrintStack(Stack<int> stack)
        {
            Stack<int> temp = new Stack<int>(stack);
            while (temp.Count > 0)
            {
                Console.WriteLine(temp.Pop());
            }
        }
    }
}
