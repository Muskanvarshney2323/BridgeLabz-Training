using System;

namespace TechVilla.DataStructures
{
    public class ArrayQueue<T>
    {
        private T[] _buffer;
        private int _head, _tail, _size;

        public ArrayQueue(int capacity = 16) { _buffer = new T[capacity]; }

        public void Enqueue(T item)
        {
            if (_size == _buffer.Length) Resize(_buffer.Length * 2);
            _buffer[_tail] = item;
            _tail = (_tail + 1) % _buffer.Length;
            _size++;
        }

        public T Dequeue()
        {
            if (_size == 0) throw new InvalidOperationException("Queue empty");
            var v = _buffer[_head];
            _head = (_head + 1) % _buffer.Length;
            _size--;
            return v;
        }

        public int Count => _size;

        private void Resize(int newSize)
        {
            var arr = new T[newSize];
            for (int i = 0; i < _size; i++) arr[i] = _buffer[(_head + i) % _buffer.Length];
            _buffer = arr; _head = 0; _tail = _size;
        }
    }
}
