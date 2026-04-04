using System;
using TechVilla.DataStructures;

namespace TechVilla.Modules
{
    public static class Module11_ServiceQueueManagement
    {
        public static void Demo()
        {
            Console.WriteLine("Module 11: Service Queue Management demo");

            // FIFO queue (array)
            var aq = new ArrayQueue<string>(4);
            aq.Enqueue("req1"); aq.Enqueue("req2"); aq.Enqueue("req3");
            Console.WriteLine("ArrayQueue dequeue:");
            Console.WriteLine(aq.Dequeue());

            // FIFO queue (linked)
            var lq = new LinkedQueue<string>();
            lq.Enqueue("L1"); lq.Enqueue("L2");
            Console.WriteLine("LinkedQueue dequeue:");
            Console.WriteLine(lq.Dequeue());

            // Priority queue for emergencies
            var pq = new PriorityQueue<string>();
            pq.Enqueue("low", 5); pq.Enqueue("emergency", 1); pq.Enqueue("med", 3);
            Console.WriteLine("PriorityQueue dequeue order:");
            while (pq.Count > 0) Console.WriteLine(pq.Dequeue());

            // Stack for undo
            var st = new Stack<string>();
            st.Push("edit1"); st.Push("edit2");
            Console.WriteLine("Stack undo:");
            Console.WriteLine(st.Pop());

            // Circular queue for resource pooling
            var cq = new CircularQueue<string>(3);
            cq.Enqueue("r1"); cq.Enqueue("r2"); cq.Enqueue("r3");
            Console.WriteLine("CircularQueue dequeue:");
            Console.WriteLine(cq.Dequeue());
        }
    }
}
