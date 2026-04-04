using System;

namespace DynamicMarketplace
{
    /// <summary>
    /// Problem 2: Dynamic Online Marketplace
    /// Demonstrates: Type Parameters, Generic Methods, Constraints
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║    PROBLEM 2: Dynamic Online Marketplace      ║");
            Console.WriteLine("║  Concepts: Type Parameters, Generic Methods   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            // Create book catalog
            ProductCatalog<BookCategory> bookCatalog = new ProductCatalog<BookCategory>("Book Store");

            // Add books
            Console.WriteLine("─── Adding Books ───");
            var book1 = new Product<BookCategory>("B001", "C# Programming", 49.99, 20, new BookCategory("Herbert Schildt", "Technology"));
            var book2 = new Product<BookCategory>("B002", "The Great Gatsby", 15.99, 50, new BookCategory("F. Scott Fitzgerald", "Fiction"));
            var book3 = new Product<BookCategory>("B003", "Clean Code", 45.99, 15, new BookCategory("Robert Martin", "Technology"));

            bookCatalog.AddProduct(book1);
            bookCatalog.AddProduct(book2);
            bookCatalog.AddProduct(book3);

            bookCatalog.DisplayAllProducts();

            // Create clothing catalog
            ProductCatalog<ClothingCategory> clothingCatalog = new ProductCatalog<ClothingCategory>("Fashion Store");

            // Add clothing items
            Console.WriteLine("─── Adding Clothing Items ───");
            var shirt = new Product<ClothingCategory>("C001", "Cotton T-Shirt", 25.99, 100, new ClothingCategory("M", "Blue"));
            var jeans = new Product<ClothingCategory>("C002", "Denim Jeans", 59.99, 75, new ClothingCategory("L", "Black"));
            var jacket = new Product<ClothingCategory>("C003", "Winter Jacket", 129.99, 30, new ClothingCategory("XL", "Red"));

            clothingCatalog.AddProduct(shirt);
            clothingCatalog.AddProduct(jeans);
            clothingCatalog.AddProduct(jacket);

            clothingCatalog.DisplayAllProducts();

            // Apply discounts using generic methods
            Console.WriteLine("─── Applying Discounts ───");
            bookCatalog.ApplyDiscount(book1, 15);
            clothingCatalog.ApplyDiscount(shirt, 20);
            clothingCatalog.ApplyDiscount(jeans, 10);

            // Display updated products
            bookCatalog.DisplayAllProducts();
            clothingCatalog.DisplayAllProducts();

            // Display catalog values
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("CATALOG VALUES:");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine($"Book Catalog Total Value: ${bookCatalog.GetTotalCatalogValue():F2}");
            Console.WriteLine($"Clothing Catalog Total Value: ${clothingCatalog.GetTotalCatalogValue():F2}");
            Console.WriteLine($"TOTAL MARKETPLACE VALUE: ${(bookCatalog.GetTotalCatalogValue() + clothingCatalog.GetTotalCatalogValue()):F2}");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            // Search functionality
            Console.WriteLine("─── Searching for 'Programming' in Books ───");
            var searchResults = bookCatalog.SearchByName("Programming");
            foreach (var product in searchResults)
            {
                product.DisplayProductInfo();
            }

            // Filter products by price
            Console.WriteLine("\n─── Books Under $50 ───");
            var affordableBooks = bookCatalog.GetProductsBelowPrice(50);
            foreach (var product in affordableBooks)
            {
                product.DisplayProductInfo();
            }
        }
    }
}
