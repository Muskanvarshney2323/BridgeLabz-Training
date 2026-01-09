using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedListProblems
{
    /// <summary>
    /// Problem 4: Singly Linked List - Inventory Management System
    /// 
    /// Design an inventory management system using a singly linked list where each node stores 
    /// information about an item such as Item Name, Item ID, Quantity, and Price.
    /// </summary>
    public class InventoryNode
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public InventoryNode Next { get; set; }

        public InventoryNode(int itemID, string itemName, int quantity, double price)
        {
            ItemID = itemID;
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
            Next = null;
        }

        public override string ToString()
        {
            return $"[ID: {ItemID}, Name: {ItemName}, Qty: {Quantity}, Price: ${Price:F2}]";
        }
    }

    public class InventoryManagementSystem
    {
        private InventoryNode head;

        public InventoryManagementSystem()
        {
            head = null;
        }

        /// <summary>
        /// Add an item at the beginning
        /// </summary>
        public void AddAtBeginning(int itemID, string itemName, int quantity, double price)
        {
            InventoryNode newNode = new InventoryNode(itemID, itemName, quantity, price);
            newNode.Next = head;
            head = newNode;
            Console.WriteLine($"Item '{itemName}' added at the beginning");
        }

        /// <summary>
        /// Add an item at the end
        /// </summary>
        public void AddAtEnd(int itemID, string itemName, int quantity, double price)
        {
            InventoryNode newNode = new InventoryNode(itemID, itemName, quantity, price);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                InventoryNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            Console.WriteLine($"Item '{itemName}' added at the end");
        }

        /// <summary>
        /// Add an item at a specific position
        /// </summary>
        public void AddAtPosition(int itemID, string itemName, int quantity, double price, int position)
        {
            if (position == 0)
            {
                AddAtBeginning(itemID, itemName, quantity, price);
                return;
            }

            InventoryNode newNode = new InventoryNode(itemID, itemName, quantity, price);
            InventoryNode current = head;
            int count = 0;

            while (current != null && count < position - 1)
            {
                current = current.Next;
                count++;
            }

            if (current == null)
            {
                Console.WriteLine("Position out of bounds");
                return;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
            Console.WriteLine($"Item '{itemName}' added at position {position}");
        }

        /// <summary>
        /// Remove an item based on Item ID
        /// </summary>
        public void RemoveByItemID(int itemID)
        {
            if (head == null)
            {
                Console.WriteLine("Inventory is empty");
                return;
            }

            if (head.ItemID == itemID)
            {
                head = head.Next;
                Console.WriteLine($"Item with ID {itemID} removed");
                return;
            }

            InventoryNode current = head;
            while (current.Next != null && current.Next.ItemID != itemID)
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                Console.WriteLine($"Item with ID {itemID} removed");
            }
            else
            {
                Console.WriteLine($"Item with ID {itemID} not found");
            }
        }

        /// <summary>
        /// Update the quantity of an item by Item ID
        /// </summary>
        public void UpdateQuantity(int itemID, int newQuantity)
        {
            InventoryNode current = head;
            while (current != null)
            {
                if (current.ItemID == itemID)
                {
                    current.Quantity = newQuantity;
                    Console.WriteLine($"Quantity for item '{current.ItemName}' updated to {newQuantity}");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine($"Item with ID {itemID} not found");
        }

        /// <summary>
        /// Search for an item based on Item ID
        /// </summary>
        public InventoryNode SearchByItemID(int itemID)
        {
            InventoryNode current = head;
            while (current != null)
            {
                if (current.ItemID == itemID)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Search for an item based on Item Name
        /// </summary>
        public List<InventoryNode> SearchByItemName(string itemName)
        {
            List<InventoryNode> results = new List<InventoryNode>();
            InventoryNode current = head;

            while (current != null)
            {
                if (current.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(current);
                }
                current = current.Next;
            }

            return results;
        }

        /// <summary>
        /// Calculate and display the total value of inventory
        /// </summary>
        public double CalculateTotalInventoryValue()
        {
            double totalValue = 0;
            InventoryNode current = head;

            while (current != null)
            {
                totalValue += current.Quantity * current.Price;
                current = current.Next;
            }

            return totalValue;
        }

        /// <summary>
        /// Sort the inventory based on Item Name (ascending/descending)
        /// </summary>
        public void SortByItemName(bool ascending = true)
        {
            if (head == null || head.Next == null)
                return;

            // Collect all items
            List<InventoryNode> items = new List<InventoryNode>();
            InventoryNode current = head;

            while (current != null)
            {
                items.Add(current);
                current = current.Next;
            }

            // Sort
            if (ascending)
            {
                items = items.OrderBy(x => x.ItemName).ToList();
            }
            else
            {
                items = items.OrderByDescending(x => x.ItemName).ToList();
            }

            // Rebuild the list
            head = items[0];
            current = head;

            for (int i = 1; i < items.Count; i++)
            {
                current.Next = items[i];
                current = current.Next;
            }
            current.Next = null;

            Console.WriteLine($"Inventory sorted by Item Name ({(ascending ? "Ascending" : "Descending")})");
        }

        /// <summary>
        /// Sort the inventory based on Price (ascending/descending)
        /// </summary>
        public void SortByPrice(bool ascending = true)
        {
            if (head == null || head.Next == null)
                return;

            // Collect all items
            List<InventoryNode> items = new List<InventoryNode>();
            InventoryNode current = head;

            while (current != null)
            {
                items.Add(current);
                current = current.Next;
            }

            // Sort
            if (ascending)
            {
                items = items.OrderBy(x => x.Price).ToList();
            }
            else
            {
                items = items.OrderByDescending(x => x.Price).ToList();
            }

            // Rebuild the list
            head = items[0];
            current = head;

            for (int i = 1; i < items.Count; i++)
            {
                current.Next = items[i];
                current = current.Next;
            }
            current.Next = null;

            Console.WriteLine($"Inventory sorted by Price ({(ascending ? "Ascending" : "Descending")})");
        }

        /// <summary>
        /// Display all items in the inventory
        /// </summary>
        public void DisplayAll()
        {
            if (head == null)
            {
                Console.WriteLine("Inventory is empty");
                return;
            }

            Console.WriteLine("\n--- Inventory Items ---");
            InventoryNode current = head;
            int count = 1;

            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Next;
                count++;
            }
            Console.WriteLine();
        }
    }

    // Example Usage
    public class InventoryManagementSystemExample
    {
        public static void Main()
        {
            InventoryManagementSystem inventory = new InventoryManagementSystem();

            // Add items
            inventory.AddAtEnd(1, "Laptop", 5, 1200.00);
            inventory.AddAtEnd(2, "Mouse", 50, 25.00);
            inventory.AddAtEnd(3, "Keyboard", 30, 75.00);
            inventory.AddAtEnd(4, "Monitor", 10, 350.00);
            inventory.AddAtEnd(5, "USB Cable", 100, 10.00);

            inventory.DisplayAll();

            // Update quantity
            inventory.UpdateQuantity(2, 45);

            // Search by ID
            Console.WriteLine("\nSearching for item with ID 3:");
            var item = inventory.SearchByItemID(3);
            Console.WriteLine(item != null ? item.ToString() : "Not found");

            // Calculate total inventory value
            double totalValue = inventory.CalculateTotalInventoryValue();
            Console.WriteLine($"\nTotal Inventory Value: ${totalValue:F2}");

            // Sort by name
            inventory.SortByItemName(true);
            inventory.DisplayAll();

            // Sort by price (descending)
            inventory.SortByPrice(false);
            inventory.DisplayAll();

            // Remove an item
            inventory.RemoveByItemID(5);
            inventory.DisplayAll();
        }
    }
}
