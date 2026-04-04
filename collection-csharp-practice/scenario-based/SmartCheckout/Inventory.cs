using System;
using System.Collections.Generic;

namespace SmartCheckout
{
    /// <summary>
    /// Manages inventory with HashMap for quick price and stock lookup.
    /// </summary>
    public class Inventory
    {
        private Dictionary<string, Item> items;

        public Inventory()
        {
            items = new Dictionary<string, Item>();
        }

        /// <summary>
        /// Add or update an item in inventory.
        /// </summary>
        public void AddItem(string itemName, decimal price, int stock)
        {
            if (items.ContainsKey(itemName))
            {
                items[itemName].Quantity += stock;
            }
            else
            {
                items[itemName] = new Item(itemName, price, stock);
            }
        }

        /// <summary>
        /// Get price of an item from the HashMap.
        /// </summary>
        public decimal GetPrice(string itemName)
        {
            if (items.ContainsKey(itemName))
            {
                return items[itemName].Price;
            }
            throw new InvalidOperationException($"Item '{itemName}' not found in inventory.");
        }

        /// <summary>
        /// Get available stock of an item.
        /// </summary>
        public int GetStock(string itemName)
        {
            if (items.ContainsKey(itemName))
            {
                return items[itemName].Quantity;
            }
            throw new InvalidOperationException($"Item '{itemName}' not found in inventory.");
        }

        /// <summary>
        /// Update stock after purchase.
        /// </summary>
        public bool DeductStock(string itemName, int quantity)
        {
            if (!items.ContainsKey(itemName))
            {
                throw new InvalidOperationException($"Item '{itemName}' not found in inventory.");
            }

            if (items[itemName].Quantity >= quantity)
            {
                items[itemName].Quantity -= quantity;
                return true;
            }

            return false; // Insufficient stock
        }

        /// <summary>
        /// Check if item exists in inventory.
        /// </summary>
        public bool ItemExists(string itemName)
        {
            return items.ContainsKey(itemName);
        }

        /// <summary>
        /// Display all items in inventory.
        /// </summary>
        public void DisplayInventory()
        {
            Console.WriteLine("\n=== INVENTORY ===");
            foreach (var item in items.Values)
            {
                Console.WriteLine(item);
            }
        }
    }
}
