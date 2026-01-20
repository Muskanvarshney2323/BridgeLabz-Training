using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartWarehouse
{
    /// <summary>
    /// Generic Storage class with constraint T : WarehouseItem
    /// Demonstrates: Generic Classes and Constraints
    /// </summary>
    public class Storage<T> where T : WarehouseItem
    {
        private List<T> items;
        private string storageLocation;

        public Storage(string location)
        {
            storageLocation = location;
            items = new List<T>();
        }

        /// <summary>
        /// Adds an item to storage
        /// </summary>
        public void AddItem(T item)
        {
            items.Add(item);
            Console.WriteLine($"✓ Added {item.Name} to {storageLocation}");
        }

        /// <summary>
        /// Removes an item from storage by ID
        /// </summary>
        public bool RemoveItem(string itemId)
        {
            var item = items.FirstOrDefault(i => i.ItemId == itemId);
            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine($"✓ Removed {item.Name} from {storageLocation}");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds an item by ID
        /// </summary>
        public T FindItem(string itemId)
        {
            return items.FirstOrDefault(i => i.ItemId == itemId);
        }

        /// <summary>
        /// Displays all items in storage
        /// </summary>
        public void DisplayAllItems()
        {
            Console.WriteLine($"\n═══════════════════════════════════════════════");
            Console.WriteLine($"Storage: {storageLocation}");
            Console.WriteLine($"Total Items: {items.Count}");
            Console.WriteLine($"═══════════════════════════════════════════════");

            if (items.Count == 0)
            {
                Console.WriteLine("No items in storage.\n");
                return;
            }

            foreach (var item in items)
            {
                item.DisplayInfo();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Calculates total value of all items in storage
        /// </summary>
        public decimal CalculateTotalValue()
        {
            return items.Sum(item => item.CalculateValue());
        }

        /// <summary>
        /// Gets item count
        /// </summary>
        public int GetItemCount()
        {
            return items.Count;
        }

        /// <summary>
        /// Updates quantity of an item
        /// </summary>
        public bool UpdateQuantity(string itemId, int newQuantity)
        {
            var item = FindItem(itemId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                Console.WriteLine($"✓ Updated quantity for {item.Name} to {newQuantity}");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all items as list
        /// </summary>
        public List<T> GetAllItems()
        {
            return new List<T>(items);
        }

        /// <summary>
        /// Searches items by name
        /// </summary>
        public List<T> SearchByName(string nameKeyword)
        {
            return items.Where(i => i.Name.ToLower().Contains(nameKeyword.ToLower())).ToList();
        }
    }
}
