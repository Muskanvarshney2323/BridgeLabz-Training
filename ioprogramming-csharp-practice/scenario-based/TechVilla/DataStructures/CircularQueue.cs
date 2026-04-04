using System;

namespace TechVilla.DataStructures
{
    public class CircularQueue<T>
    {
        private T[] _buf;
        private int _head, _tail, _count;
        public CircularQueue(int capacity = 8) { _buf = new T[capacity]; }

        public void Enqueue(T item)
        {
            if (_count == _buf.Length) Resize(_buf.Length * 2);
            _buf[_tail] = item;
            _tail = (_tail + 1) % _buf.Length;
            _count++;
        }
        public T Dequeue()
        {
            if (_count == 0) throw new InvalidOperationException("empty");
            var v = _buf[_head];
            _head = (_head + 1) % _buf.Length;
            _count--;
            return v;
        }

        private void Resize(int n)
        {
            var arr = new T[n];
            for (int i = 0; i < _count; i++) arr[i] = _buf[(_head + i) % _buf.Length];
            _buf = arr; _head = 0; _tail = _count;
        }
    }
}
