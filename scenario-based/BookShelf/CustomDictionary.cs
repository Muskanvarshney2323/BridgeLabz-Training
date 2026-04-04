using System;

public class CustomDictionary
{
    private DictionaryEntry[] entries;
    private int count;
    private int capacity;
    private const int INITIAL_CAPACITY = 16;
    private const float LOAD_FACTOR = 0.75f;

    public CustomDictionary()
    {
        this.capacity = INITIAL_CAPACITY;
        this.entries = new DictionaryEntry[capacity];
        this.count = 0;
    }

    // Hash function to compute index
    private int GetHashCode(string key)
    {
        if (key == null)
            return 0;

        int hash = 0;
        foreach (char c in key)
        {
            hash = hash * 31 + c;
        }
        return Math.Abs(hash) % capacity;
    }

    // Get value by key
    public BookLinkedList Get(string key)
    {
        if (key == null)
            return null;

        int index = GetHashCode(key);
        int startIndex = index;

        // Linear probing to find the key
        while (entries[index] != null)
        {
            if (entries[index].Key.Equals(key))
            {
                return entries[index].Value;
            }
            index = (index + 1) % capacity;

            // Prevent infinite loop
            if (index == startIndex)
                return null;
        }

        return null;
    }

    // Add or update key-value pair
    public void Put(string key, BookLinkedList value)
    {
        if (key == null)
        {
            Console.WriteLine("Key cannot be null.");
            return;
        }

        // Check if resize is needed
        if ((float)count / capacity >= LOAD_FACTOR)
        {
            Resize();
        }

        int index = GetHashCode(key);
        int startIndex = index;

        // Linear probing to find empty slot or existing key
        while (entries[index] != null)
        {
            if (entries[index].Key.Equals(key))
            {
                // Update existing key
                entries[index].Value = value;
                return;
            }
            index = (index + 1) % capacity;

            // Prevent infinite loop
            if (index == startIndex)
            {
                Console.WriteLine("Dictionary is full!");
                return;
            }
        }

        // Add new entry
        entries[index] = new DictionaryEntry(key, value);
        count++;
    }

    // Remove key-value pair
    public bool Remove(string key)
    {
        if (key == null)
            return false;

        int index = GetHashCode(key);
        int startIndex = index;

        // Linear probing to find the key
        while (entries[index] != null)
        {
            if (entries[index].Key.Equals(key))
            {
                entries[index] = null;
                count--;

                // Rehash entries after removed position
                index = (index + 1) % capacity;
                while (entries[index] != null)
                {
                    DictionaryEntry temp = entries[index];
                    entries[index] = null;
                    count--;
                    Put(temp.Key, temp.Value);
                    index = (index + 1) % capacity;
                }

                return true;
            }
            index = (index + 1) % capacity;

            // Prevent infinite loop
            if (index == startIndex)
                return false;
        }

        return false;
    }

    // Check if key exists
    public bool ContainsKey(string key)
    {
        return Get(key) != null;
    }

    // Get all keys
    public string[] GetAllKeys()
    {
        string[] keys = new string[count];
        int index = 0;

        for (int i = 0; i < capacity; i++)
        {
            if (entries[i] != null)
            {
                keys[index++] = entries[i].Key;
            }
        }

        return keys;
    }

    // Get dictionary size
    public int GetCount()
    {
        return count;
    }

    // Check if dictionary is empty
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Resize the dictionary when load factor is exceeded
    private void Resize()
    {
        int newCapacity = capacity * 2;
        DictionaryEntry[] oldEntries = entries;
        entries = new DictionaryEntry[newCapacity];
        capacity = newCapacity;
        count = 0;

        // Rehash all existing entries
        for (int i = 0; i < oldEntries.Length; i++)
        {
            if (oldEntries[i] != null)
            {
                Put(oldEntries[i].Key, oldEntries[i].Value);
            }
        }

        Console.WriteLine($"Dictionary resized to capacity: {capacity}");
    }

    // Clear the dictionary
    public void Clear()
    {
        entries = new DictionaryEntry[capacity];
        count = 0;
    }

    // Display all entries
    public void DisplayEntries()
    {
        Console.WriteLine("\n--- Dictionary Contents ---");
        if (IsEmpty())
        {
            Console.WriteLine("Dictionary is empty.");
            return;
        }

        for (int i = 0; i < capacity; i++)
        {
            if (entries[i] != null)
            {
                Console.WriteLine($"Key: {entries[i].Key}, Value: {entries[i].Value}");
            }
        }
    }
}
