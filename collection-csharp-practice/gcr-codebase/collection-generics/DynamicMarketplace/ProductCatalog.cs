using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicMarketplace
{
    /// <summary>
    /// ProductCatalog class demonstrating Generic Methods and Constraints
    /// </summary>
    public class ProductCatalog<T> where T : Category
    {
        private List<Product<T>> products;
        private string catalogName;

        public ProductCatalog(string name)
        {
            catalogName = name;
            products = new List<Product<T>>();
        }

        /// <summary>
        /// Adds a product to the catalog
        /// </summary>
        public void AddProduct(Product<T> product)
        {
            products.Add(product);
            Console.WriteLine($"✓ Added {product.ProductName} to {catalogName}");
        }

        /// <summary>
        /// Generic method to apply discount on products
        /// Demonstrates: Generic Methods with Constraints
        /// </summary>
        public void ApplyDiscount<U>(U product, double percentage) where U : Product<T>
        {
            if (percentage < 0 || percentage > 100)
            {
                Console.WriteLine($"❌ Invalid discount percentage: {percentage}%");
                return;
            }

            double discountAmount = product.OriginalPrice * (percentage / 100.0);
            product.CurrentPrice = product.OriginalPrice - discountAmount;
            Console.WriteLine($"✓ Applied {percentage}% discount to {product.ProductName}. New Price: ${product.CurrentPrice:F2}");
        }

        /// <summary>
        /// Generic method to apply bulk discount
        /// </summary>
        public void ApplyBulkDiscount<U>(List<U> productList, double percentage) where U : Product<T>
        {
            Console.WriteLine($"\n─── Applying {percentage}% discount to {productList.Count} items ───");
            foreach (var product in productList)
            {
                ApplyDiscount(product, percentage);
            }
        }

        /// <summary>
        /// Display all products in catalog
        /// </summary>
        public void DisplayAllProducts()
        {
            Console.WriteLine($"\n═══════════════════════════════════════════════");
            Console.WriteLine($"Catalog: {catalogName}");
            Console.WriteLine($"Total Products: {products.Count}");
            Console.WriteLine($"═══════════════════════════════════════════════");

            if (products.Count == 0)
            {
                Console.WriteLine("No products in catalog.\n");
                return;
            }

            foreach (var product in products)
            {
                product.DisplayProductInfo();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Find product by ID
        /// </summary>
        public Product<T> FindProduct(string productId)
        {
            return products.FirstOrDefault(p => p.ProductId == productId);
        }

        /// <summary>
        /// Get all products with price above specified amount
        /// </summary>
        public List<Product<T>> GetProductsAbovePrice(double minPrice)
        {
            return products.Where(p => p.CurrentPrice >= minPrice).ToList();
        }

        /// <summary>
        /// Get all products with price below specified amount
        /// </summary>
        public List<Product<T>> GetProductsBelowPrice(double maxPrice)
        {
            return products.Where(p => p.CurrentPrice <= maxPrice).ToList();
        }

        /// <summary>
        /// Get total catalog value
        /// </summary>
        public double GetTotalCatalogValue()
        {
            return products.Sum(p => p.CurrentPrice * p.StockQuantity);
        }

        /// <summary>
        /// Search products by name
        /// </summary>
        public List<Product<T>> SearchByName(string keyword)
        {
            return products.Where(p => p.ProductName.ToLower().Contains(keyword.ToLower())).ToList();
        }
    }
}
