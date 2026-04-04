namespace AddressBookSystem.Utilities
{
    /// <summary>
    /// Generic collection wrapper demonstrating generic type constraints and collections
    /// </summary>
    public class GenericAddressBookCollection<T> where T : class
    {
        private List<T> items;

        public GenericAddressBookCollection()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            if (item != null)
            {
                items.Add(item);
            }
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public T? Get(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                return items[index];
            }
            return null;
        }

        public List<T> GetAll()
        {
            return new List<T>(items);
        }

        public int Count()
        {
            return items.Count;
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }
    }
}
