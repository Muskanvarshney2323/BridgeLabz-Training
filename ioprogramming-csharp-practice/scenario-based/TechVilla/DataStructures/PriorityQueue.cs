using System;
using System.Collections.Generic;

namespace TechVilla.DataStructures
{
    public class PriorityQueue<T>
    {
        private readonly List<(int priority, T item)> _items = new List<(int, T)>();
        public void Enqueue(T item, int priority)
        {
            _items.Add((priority, item));
            _items.Sort((a, b) => a.priority.CompareTo(b.priority));
        }
        public T Dequeue()
        {
            if (_items.Count == 0) throw new InvalidOperationException("empty");
            var v = _items[0].item; _items.RemoveAt(0); return v;
        }
        public int Count => _items.Count;
    }
}
