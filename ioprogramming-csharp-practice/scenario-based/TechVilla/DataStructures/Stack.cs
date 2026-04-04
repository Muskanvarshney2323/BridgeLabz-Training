using System;
using System.Collections.Generic;

namespace TechVilla.DataStructures
{
    public class Stack<T>
    {
        private readonly List<T> _inner = new List<T>();
        public void Push(T item) => _inner.Add(item);
        public T Pop() { var v = _inner[^1]; _inner.RemoveAt(_inner.Count - 1); return v; }
        public T Peek() => _inner[^1];
        public bool IsEmpty() => _inner.Count == 0;
        public int Count => _inner.Count;
    }
}
