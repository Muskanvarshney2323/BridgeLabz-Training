using System.Collections.Generic;

namespace TechVilla.DataStructures
{
    public class LinkedQueue<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();
        public void Enqueue(T item) => _list.AddLast(item);
        public T Dequeue() { var v = _list.First!.Value; _list.RemoveFirst(); return v; }
        public int Count => _list.Count;
    }
}
