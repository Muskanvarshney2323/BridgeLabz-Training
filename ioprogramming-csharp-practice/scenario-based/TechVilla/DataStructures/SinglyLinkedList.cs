using System;
using System.Collections.Generic;

namespace TechVilla.DataStructures
{
    public class SinglyLinkedListNode<T>
    {
        public T Value { get; set; }
        public SinglyLinkedListNode<T>? Next { get; set; }
        public SinglyLinkedListNode(T value) => Value = value;
    }

    public class SinglyLinkedList<T>
    {
        public SinglyLinkedListNode<T>? Head { get; private set; }

        public void AddLast(T value)
        {
            var node = new SinglyLinkedListNode<T>(value);
            if (Head == null) { Head = node; return; }
            var cur = Head;
            while (cur.Next != null) cur = cur.Next;
            cur.Next = node;
        }

        public bool Remove(T value)
        {
            if (Head == null) return false;
            if (EqualityComparer<T>.Default.Equals(Head.Value, value))
            {
                Head = Head.Next;
                return true;
            }
            var prev = Head;
            var cur = Head.Next;
            while (cur != null)
            {
                if (EqualityComparer<T>.Default.Equals(cur.Value, value))
                {
                    prev.Next = cur.Next;
                    return true;
                }
                prev = cur;
                cur = cur.Next;
            }
            return false;
        }

        public IEnumerable<T> Traverse()
        {
            var cur = Head;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }
    }
}
