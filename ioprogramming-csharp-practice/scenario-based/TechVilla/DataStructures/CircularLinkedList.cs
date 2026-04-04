using System;
using System.Collections.Generic;

namespace TechVilla.DataStructures
{
    public class CircularLinkedListNode<T>
    {
        public T Value { get; set; }
        public CircularLinkedListNode<T>? Next { get; set; }
        public CircularLinkedListNode(T value) => Value = value;
    }

    public class CircularLinkedList<T>
    {
        public CircularLinkedListNode<T>? Tail { get; private set; }

        public void Add(T value)
        {
            var node = new CircularLinkedListNode<T>(value);
            if (Tail == null)
            {
                Tail = node;
                node.Next = node;
                return;
            }
            node.Next = Tail.Next;
            Tail.Next = node;
            Tail = node;
        }

        public bool Remove(T value)
        {
            if (Tail == null) return false;
            var prev = Tail;
            var cur = Tail.Next;
            do
            {
                if (EqualityComparer<T>.Default.Equals(cur.Value, value))
                {
                    if (cur == prev) { Tail = null; return true; }
                    prev.Next = cur.Next;
                    if (cur == Tail) Tail = prev;
                    return true;
                }
                prev = cur;
                cur = cur.Next;
            } while (cur != Tail.Next);
            return false;
        }

        public IEnumerable<T> Traverse(int? limit = null)
        {
            if (Tail == null) yield break;
            var start = Tail.Next;
            var cur = start;
            int count = 0;
            do
            {
                yield return cur.Value;
                cur = cur.Next;
                count++;
            } while (cur != start && (limit == null || count < limit));
        }
    }
}
