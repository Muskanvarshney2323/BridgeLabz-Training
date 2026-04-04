using System;
using TechVilla.DataStructures;

namespace TechVilla.Modules
{
    public static class Module10_CitizenNetworkSystem
    {
        public static void Demo()
        {
            Console.WriteLine("Module 10: Citizen Network System demo");

            // Singly linked list — queue at service center
            var queue = new SinglyLinkedList<string>();
            queue.AddLast("Alice"); queue.AddLast("Bob"); queue.AddLast("Charlie");
            Console.WriteLine("Singly linked list queue:");
            foreach (var n in queue.Traverse()) Console.WriteLine(" - " + n);

            // Doubly linked list — navigation
            var nav = new DoublyLinkedList<string>();
            nav.AddLast("Alice"); nav.AddLast("Becky"); nav.AddLast("Carlos");
            Console.WriteLine("Doubly linked list forward:");
            foreach (var n in nav.TraverseForward()) Console.WriteLine(" > " + n);
            Console.WriteLine("Doubly linked list backward:");
            foreach (var n in nav.TraverseBackward()) Console.WriteLine(" < " + n);

            // Circular linked list — round robin
            var round = new CircularLinkedList<string>();
            round.Add("SvcA"); round.Add("SvcB"); round.Add("SvcC");
            Console.WriteLine("Circular list round-robin (limited):");
            int i = 0;
            foreach (var s in round.Traverse(6)) { Console.WriteLine(" * " + s); if (++i > 10) break; }
        }
    }
}
