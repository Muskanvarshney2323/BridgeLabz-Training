using System;

public class CustomHashSet
{
    private string[] items;
    private int count;
    private int capacity;
    private const int INITIAL_CAPACITY = 16;
    private const float LOAD_FACTOR = 0.75f;

    public CustomHashSet()
    {
        this.capacity = INITIAL_CAPACITY;
        this.items = new string[capacity];
        this.count = 0;
    }

    // Hash function
    private int GetHashCode(string item)
    {
        if (item == null)
            return 0;

        int hash = 0;
        foreach (char c in item)
        {
            hash = hash * 31 + c;
        }
        return Math.Abs(hash) % capacity;
    }

    // Add item to set
    public bool Add(string item)
    {
        if (item == null)
            return false;

        if ((float)count / capacity >= LOAD_FACTOR)
        {
            Resize();
        }

        int index = GetHashCode(item);
        int startIndex = index;

        // Linear probing
        while (items[index] != null)
        {
            if (items[index].Equals(item))
            {
                return false; // Already exists
            }
            index = (index + 1) % capacity;

            if (index == startIndex)
                return false;
        }

        items[index] = item;
        count++;
        return true;
    }

    // Remove item from set
    public bool Remove(string item)
    {
        if (item == null)
            return false;

        int index = GetHashCode(item);
        int startIndex = index;

        while (items[index] != null)
        {
            if (items[index].Equals(item))
            {
                items[index] = null;
                count--;

                // Rehash after deletion
                index = (index + 1) % capacity;
                while (items[index] != null)
                {
                    string temp = items[index];
                    items[index] = null;
                    count--;
                    Add(temp);
                    index = (index + 1) % capacity;
                }

                return true;
            }
            index = (index + 1) % capacity;

            if (index == startIndex)
                return false;
        }

        return false;
    }

    // Check if item exists
    public bool Contains(string item)
    {
        if (item == null)
            return false;

        int index = GetHashCode(item);
        int startIndex = index;

        while (items[index] != null)
        {
            if (items[index].Equals(item))
            {
                return true;
            }
            index = (index + 1) % capacity;

            if (index == startIndex)
                return false;
        }

        return false;
    }

    // Get count of items
    public int GetCount()
    {
        return count;
    }

    // Check if set is empty
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Resize the set
    private void Resize()
    {
        int newCapacity = capacity * 2;
        string[] oldItems = items;
        items = new string[newCapacity];
        capacity = newCapacity;
        count = 0;

        for (int i = 0; i < oldItems.Length; i++)
        {
            if (oldItems[i] != null)
            {
                Add(oldItems[i]);
            }
        }

        Console.WriteLine($"HashSet resized to capacity: {capacity}");
    }

    // Clear the set
    public void Clear()
    {
        items = new string[capacity];
        count = 0;
    }

    // Get all items
    public string[] GetAllItems()
    {
        string[] result = new string[count];
        int index = 0;

        for (int i = 0; i < capacity; i++)
        {
            if (items[i] != null)
            {
                result[index++] = items[i];
            }
        }

        return result;
    }
}
