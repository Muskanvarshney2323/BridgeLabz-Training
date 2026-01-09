using System;
using System.Collections.Generic;

namespace StackQueueProblems
{
    /// <summary>
    /// Problem 1: Implement a Queue Using Stacks
    /// 
    /// Design a queue using two stacks such that enqueue and dequeue operations are performed efficiently.
    /// 
    /// Hint: Use one stack for enqueue and another stack for dequeue. 
    /// Transfer elements between stacks as needed.
    /// </summary>
    public class QueueUsingStacks<T>
    {
        private Stack<T> enqueueStack;
        private Stack<T> dequeueStack;

        public QueueUsingStacks()
        {
            enqueueStack = new Stack<T>();
            dequeueStack = new Stack<T>();
        }

        /// <summary>
        /// Add an element to the queue - Time Complexity: O(1)
        /// </summary>
        public void Enqueue(T element)
        {
            enqueueStack.Push(element);
        }

        /// <summary>
        /// Remove and return an element from the queue - Amortized Time Complexity: O(1)
        /// </summary>
        public T Dequeue()
        {
            if (dequeueStack.Count == 0)
            {
                // Transfer all elements from enqueueStack to dequeueStack
                while (enqueueStack.Count > 0)
                {
                    dequeueStack.Push(enqueueStack.Pop());
                }
            }

            if (dequeueStack.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return dequeueStack.Pop();
        }

        /// <summary>
        /// Peek at the front element without removing it
        /// </summary>
        public T Peek()
        {
            if (dequeueStack.Count == 0)
            {
                while (enqueueStack.Count > 0)
                {
                    dequeueStack.Push(enqueueStack.Pop());
                }
            }

            if (dequeueStack.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return dequeueStack.Peek();
        }

        /// <summary>
        /// Check if the queue is empty
        /// </summary>
        public bool IsEmpty => enqueueStack.Count == 0 && dequeueStack.Count == 0;

        /// <summary>
        /// Get the number of elements in the queue
        /// </summary>
        public int Count => enqueueStack.Count + dequeueStack.Count;
    }

    // Example Usage
    public class QueueUsingStacksExample
    {
        public static void Main()
        {
            QueueUsingStacks<int> queue = new QueueUsingStacks<int>();

            // Enqueue elements
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            Console.WriteLine("Queue operations:");
            Console.WriteLine($"Peek: {queue.Peek()}"); // Output: 1
            Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // Output: 1
            Console.WriteLine($"Dequeue: {queue.Dequeue()}"); // Output: 2
            Console.WriteLine($"Peek: {queue.Peek()}"); // Output: 3
            Console.WriteLine($"Queue Count: {queue.Count}"); // Output: 2
        }
    }
}
