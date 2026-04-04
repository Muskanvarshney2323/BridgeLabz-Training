using System;

namespace SmartWarehouse
{
    /// <summary>
    /// Problem 1: Smart Warehouse Management System
    /// Demonstrates: Generic Classes, Constraints, Variance
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║   PROBLEM 1: Smart Warehouse Management       ║");
            Console.WriteLine("║   Concepts: Generics, Constraints, Variance   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            // Create storage locations for different item types
            Storage<Electronics> electronicsStorage = new Storage<Electronics>("Electronics Section");
            Storage<Groceries> groceriesStorage = new Storage<Groceries>("Grocery Section");
            Storage<Furniture> furnitureStorage = new Storage<Furniture>("Furniture Section");

            // Add Electronics
            Console.WriteLine("─── Adding Electronics ───");
            electronicsStorage.AddItem(new Electronics("E001", "Laptop", 999.99, 5, "Dell", 24));
            electronicsStorage.AddItem(new Electronics("E002", "Smartphone", 699.99, 10, "Samsung", 12));
            electronicsStorage.AddItem(new Electronics("E003", "Tablet", 449.99, 8, "Apple", 24));

            // Add Groceries
            Console.WriteLine("\n─── Adding Groceries ───");
            groceriesStorage.AddItem(new Groceries("G001", "Organic Apples", 3.99, 50, "2024-02-15", true));
            groceriesStorage.AddItem(new Groceries("G002", "Milk", 4.49, 30, "2024-01-25", false));
            groceriesStorage.AddItem(new Groceries("G003", "Whole Wheat Bread", 2.99, 20, "2024-01-22", true));

            // Add Furniture
            Console.WriteLine("\n─── Adding Furniture ───");
            furnitureStorage.AddItem(new Furniture("F001", "Office Chair", 249.99, 12, "Leather", "Modern"));
            furnitureStorage.AddItem(new Furniture("F002", "Wooden Table", 499.99, 5, "Oak", "Classic"));
            furnitureStorage.AddItem(new Furniture("F003", "Bookshelf", 179.99, 8, "Pine", "Contemporary"));

            // Display all items in each storage
            electronicsStorage.DisplayAllItems();
            groceriesStorage.DisplayAllItems();
            furnitureStorage.DisplayAllItems();

            // Display total values
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("INVENTORY VALUES:");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine($"Electronics Total Value: ${electronicsStorage.CalculateTotalValue():F2}");
            Console.WriteLine($"Groceries Total Value: ${groceriesStorage.CalculateTotalValue():F2}");
            Console.WriteLine($"Furniture Total Value: ${furnitureStorage.CalculateTotalValue():F2}");
            Console.WriteLine($"GRAND TOTAL: ${(electronicsStorage.CalculateTotalValue() + groceriesStorage.CalculateTotalValue() + furnitureStorage.CalculateTotalValue()):F2}");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            // Update quantities
            Console.WriteLine("─── Updating Quantities ───");
            electronicsStorage.UpdateQuantity("E001", 3);
            groceriesStorage.UpdateQuantity("G001", 45);
            furnitureStorage.UpdateQuantity("F002", 4);

            // Search functionality
            Console.WriteLine("\n─── Searching for Items ───");
            var searchResults = electronicsStorage.SearchByName("phone");
            Console.WriteLine($"\nSearch Results for 'phone':");
            foreach (var item in searchResults)
            {
                item.DisplayInfo();
            }

            // Remove items
            Console.WriteLine("\n─── Removing Items ───");
            electronicsStorage.RemoveItem("E003");
            groceriesStorage.RemoveItem("G002");

            // Final inventory display
            Console.WriteLine("\n─── FINAL INVENTORY ───");
            electronicsStorage.DisplayAllItems();
            groceriesStorage.DisplayAllItems();
            furnitureStorage.DisplayAllItems();
        }
    }
}
