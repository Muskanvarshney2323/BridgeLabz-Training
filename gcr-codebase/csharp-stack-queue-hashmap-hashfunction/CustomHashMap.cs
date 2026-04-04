using System;
using System.Collections.Generic;

namespace HashMapProblems
{
   
    public class CustomHashMap<TKey, TValue>
    {
        private const int DEFAULT_CAPACITY = 16;
        private const float LOAD_FACTOR = 0.75f;

        private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
        private int size;

        public CustomHashMap()
        {
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[DEFAULT_CAPACITY];
            size = 0;
        }

        public CustomHashMap(int capacity)
        {
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            size = 0;
        }

        
        /// Hash function to map key to bucket index
   
        private int GetBucketIndex(TKey key)
        {
            if (key == null)
                return 0;

            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode) % buckets.Length;
        }


        /// Get the bucket (linked list) for a key

        private LinkedList<KeyValuePair<TKey, TValue>> GetBucket(TKey key)
        {
            int index = GetBucketIndex(key);

            if (buckets[index] == null)
            {
                buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            return buckets[index];
        }

        /// Insert or update a key-value pair

        public void Put(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            LinkedList<KeyValuePair<TKey, TValue>> bucket = GetBucket(key);

            // Check if key already exists
            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    // Update existing value
                    bucket.Remove(node);
                    bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
                    return;
                }
                node = node.Next;
            }

            // Add new key-value pair
            bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
            size++;

            // Check if resizing is needed
            if (size >= buckets.Length * LOAD_FACTOR)
            {
                Resize();
            }
        }

        /// Get value associated with a key

        public TValue Get(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            LinkedList<KeyValuePair<TKey, TValue>> bucket = GetBucket(key);

            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    return node.Value.Value;
                }
                node = node.Next;
            }

            throw new KeyNotFoundException($"Key '{key}' not found in hash map");
        }


        /// Try to get a value without throwing exception

        public bool TryGet(TKey key, out TValue value)
        {
            value = default;

            if (key == null)
                return false;

            LinkedList<KeyValuePair<TKey, TValue>> bucket = GetBucket(key);

            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    value = node.Value.Value;
                    return true;
                }
                node = node.Next;
            }

            return false;
        }

        /// Remove a key-value pair

        public bool Remove(TKey key)
        {
            if (key == null)
                return false;

            LinkedList<KeyValuePair<TKey, TValue>> bucket = GetBucket(key);

            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    bucket.Remove(node);
                    size--;
                    return true;
                }
                node = node.Next;
            }

            return false;
        }

        /// Check if a key exists
 
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                return false;

            LinkedList<KeyValuePair<TKey, TValue>> bucket = GetBucket(key);

            var node = bucket.First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    return true;
                }
                node = node.Next;
            }

            return false;
        }

        /// <summary>
        /// Resize the hash map when load factor is exceeded
        /// </summary>
        private void Resize()
        {
            LinkedList<KeyValuePair<TKey, TValue>>[] oldBuckets = buckets;
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[oldBuckets.Length * 2];
            size = 0;

            foreach (var bucket in oldBuckets)
            {
                if (bucket != null)
                {
                    foreach (var kvp in bucket)
                    {
                        Put(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Get the number of key-value pairs
        /// </summary>
        public int Size => size;

        /// <summary>
        /// Clear all entries
        /// </summary>
        public void Clear()
        {
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[DEFAULT_CAPACITY];
            size = 0;
        }

        /// <summary>
        /// Get all keys
        /// </summary>
        public List<TKey> GetAllKeys()
        {
            List<TKey> keys = new List<TKey>();

            foreach (var bucket in buckets)
            {
                if (bucket != null)
                {
                    foreach (var kvp in bucket)
                    {
                        keys.Add(kvp.Key);
                    }
                }
            }

            return keys;
        }

        /// <summary>
        /// Get all values
        /// </summary>
        public List<TValue> GetAllValues()
        {
            List<TValue> values = new List<TValue>();

            foreach (var bucket in buckets)
            {
                if (bucket != null)
                {
                    foreach (var kvp in bucket)
                    {
                        values.Add(kvp.Value);
                    }
                }
            }

            return values;
        }
    }

    // Example Usage
    public class CustomHashMapExample
    {
        public static void Main()
        {
            CustomHashMap<string, int> map = new CustomHashMap<string, int>();

            Console.WriteLine("=== Hash Map Operations ===\n");

            // Insert operations
            map.Put("apple", 1);
            map.Put("banana", 2);
            map.Put("cherry", 3);
            map.Put("date", 4);
            map.Put("elderberry", 5);

            Console.WriteLine($"Size after insertions: {map.Size}");

            // Get operations
            Console.WriteLine($"\nRetrieving values:");
            Console.WriteLine($"apple: {map.Get("apple")}");
            Console.WriteLine($"banana: {map.Get("banana")}");
            Console.WriteLine($"cherry: {map.Get("cherry")}");

            // Update operation
            Console.WriteLine($"\nUpdating apple to 10...");
            map.Put("apple", 10);
            Console.WriteLine($"apple: {map.Get("apple")}");

            // Contains key
            Console.WriteLine($"\nContains 'apple': {map.ContainsKey("apple")}");
            Console.WriteLine($"Contains 'grape': {map.ContainsKey("grape")}");

            // TryGet
            Console.WriteLine($"\nUsing TryGet:");
            if (map.TryGet("banana", out int value))
            {
                Console.WriteLine($"banana found: {value}");
            }

            // Remove operation
            Console.WriteLine($"\nRemoving 'date'...");
            map.Remove("date");
            Console.WriteLine($"Size after removal: {map.Size}");

            // Get all keys and values
            Console.WriteLine($"\nAll keys: {string.Join(", ", map.GetAllKeys())}");
            Console.WriteLine($"All values: {string.Join(", ", map.GetAllValues())}");

            // Clear
            Console.WriteLine($"\nClearing map...");
            map.Clear();
            Console.WriteLine($"Size after clear: {map.Size}");
        }
    }
}
