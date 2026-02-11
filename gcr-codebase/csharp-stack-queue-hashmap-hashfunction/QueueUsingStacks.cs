using System;
using System.Collections.Generic;

namespace StackQueueProblems
{
    
    public class QueueUsingStacks<T>
    {
        private Stack<T> enqueueStack;
        private Stack<T> dequeueStack;

        public QueueUsingStacks()
        {
            enqueueStack = new Stack<T>();
            dequeueStack = new Stack<T>();
        }

    
        public void Enqueue(T element)
        {
            enqueueStack.Push(element);
        }

   
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

 
        public bool IsEmpty => enqueueStack.Count == 0 && dequeueStack.Count == 0;


        public int Count => enqueueStack.Count + dequeueStack.Count;
    }

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
