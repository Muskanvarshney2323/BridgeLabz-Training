using System;
using System.Collections.Generic;

namespace TechVilla.DataStructures
{
    public class DoublyLinkedListNode<T>
    {
        public T Value { get; set; }
        public DoublyLinkedListNode<T>? Prev { get; set; }
        public DoublyLinkedListNode<T>? Next { get; set; }
        public DoublyLinkedListNode(T value) => Value = value;
    }

    public class DoublyLinkedList<T>
    {
        public DoublyLinkedListNode<T>? Head { get; private set; }
        public DoublyLinkedListNode<T>? Tail { get; private set; }

        public void AddLast(T value)
        {
            var node = new DoublyLinkedListNode<T>(value);
            if (Head == null) { Head = Tail = node; return; }
            Tail!.Next = node;
            node.Prev = Tail;
            Tail = node;
        }

        public bool Remove(T value)
        {
            var cur = Head;
            while (cur != null)
            {
                if (EqualityComparer<T>.Default.Equals(cur.Value, value))
                {
                    if (cur.Prev != null) cur.Prev.Next = cur.Next; else Head = cur.Next;
                    if (cur.Next != null) cur.Next.Prev = cur.Prev; else Tail = cur.Prev;
                    return true;
                }
                cur = cur.Next;
            }
            return false;
        }

        public IEnumerable<T> TraverseForward()
        {
            var cur = Head;
            while (cur != null) { yield return cur.Value; cur = cur.Next; }
        }

        public IEnumerable<T> TraverseBackward()
        {
            var cur = Tail;
            while (cur != null) { yield return cur.Value; cur = cur.Prev; }
        }
    }
}
