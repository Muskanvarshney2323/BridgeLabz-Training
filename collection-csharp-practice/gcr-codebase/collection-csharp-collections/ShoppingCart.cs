using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Problem 3: Implement a Shopping Cart
/// Use Dictionary<string, double> for prices, maintain order with sorted display.
/// </summary>
class ShoppingCartProgram
{
    class CartItem
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public CartItem(string productName, double price, int quantity = 1)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public double GetTotal()
        {
            return Price * Quantity;
        }

        public override string ToString()
        {
            return $"{ProductName} | Rs.{Price} x {Quantity} = Rs.{GetTotal()}";
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║          Implement a Shopping Cart                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Product prices
            Dictionary<string, double> productPrices = new Dictionary<string, double>
            {
                { "Laptop", 50000 },
                { "Mouse", 500 },
                { "Keyboard", 1500 },
                { "Monitor", 15000 },
                { "USB Cable", 200 },
                { "Headphones", 3000 },
                { "Webcam", 2500 }
            };

            // Shopping cart
            Dictionary<string, CartItem> cart = new Dictionary<string, CartItem>();

            Console.WriteLine("=== SHOPPING CART OPERATIONS ===\n");

            // Add items
            Console.WriteLine("Adding items to cart...");
            AddToCart(cart, productPrices, "Laptop", 1);
            AddToCart(cart, productPrices, "Mouse", 2);
            AddToCart(cart, productPrices, "Keyboard", 1);
            AddToCart(cart, productPrices, "Monitor", 1);
            AddToCart(cart, productPrices, "Headphones", 1);
            Console.WriteLine();

            // Display cart
            Console.WriteLine("=== SHOPPING CART (In Order of Addition) ===");
            foreach (var item in cart.Values)
            {
                Console.WriteLine($"  {item}");
            }
            Console.WriteLine();

            // Display sorted by price
            Console.WriteLine("=== CART ITEMS (Sorted by Price) ===");
            var sortedByPrice = cart.Values.OrderBy(x => x.Price).ToList();
            foreach (var item in sortedByPrice)
            {
                Console.WriteLine($"  {item}");
            }
            Console.WriteLine();

            // Cart summary
            Console.WriteLine("=== CART SUMMARY ===");
            double subtotal = cart.Values.Sum(x => x.GetTotal());
            double tax = subtotal * 0.1; // 10% tax
            double total = subtotal + tax;

            Console.WriteLine($"Subtotal: Rs.{subtotal:F2}");
            Console.WriteLine($"Tax (10%): Rs.{tax:F2}");
            Console.WriteLine($"Total: Rs.{total:F2}");
            Console.WriteLine();

            // Update quantity
            Console.WriteLine("=== UPDATING QUANTITY ===");
            Console.WriteLine("Increasing Mouse quantity from 2 to 3...");
            UpdateQuantity(cart, "Mouse", 3);
            Console.WriteLine($"New total: Rs.{cart.Values.Sum(x => x.GetTotal()) + (cart.Values.Sum(x => x.GetTotal()) * 0.1):F2}");
            Console.WriteLine();

            // Remove item
            Console.WriteLine("=== REMOVING ITEM ===");
            Console.WriteLine("Removing USB Cable from cart...");
            RemoveFromCart(cart, "USB Cable");
            Console.WriteLine("Item removed.");
            Console.WriteLine();

            // Updated cart
            Console.WriteLine("=== UPDATED CART ===");
            foreach (var item in cart.Values)
            {
                Console.WriteLine($"  {item}");
            }
            Console.WriteLine();

            // Inventory check
            Console.WriteLine("=== INVENTORY CHECK ===");
            Console.WriteLine("Available Products:");
            var sortedInventory = productPrices.OrderBy(x => x.Value);
            foreach (var product in sortedInventory)
            {
                string inCart = cart.ContainsKey(product.Key) ? $"({cart[product.Key].Quantity} in cart)" : "";
                Console.WriteLine($"  {product.Key}: Rs.{product.Value} {inCart}");
            }
            Console.WriteLine();

            // Final calculation
            Console.WriteLine("=== FINAL BILL ===");
            subtotal = cart.Values.Sum(x => x.GetTotal());
            tax = subtotal * 0.1;
            total = subtotal + tax;

            foreach (var item in cart.Values)
            {
                Console.WriteLine($"  {item}");
            }
            Console.WriteLine($"\n  Subtotal: Rs.{subtotal:F2}");
            Console.WriteLine($"  Tax: Rs.{tax:F2}");
            Console.WriteLine($"  Total: Rs.{total:F2}");
            Console.WriteLine();

            Console.WriteLine("✓ Shopping Cart system completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static void AddToCart(Dictionary<string, CartItem> cart, Dictionary<string, double> prices, string product, int quantity)
    {
        if (!prices.ContainsKey(product))
        {
            Console.WriteLine($"  ✗ Product '{product}' not available");
            return;
        }

        if (cart.ContainsKey(product))
        {
            cart[product].Quantity += quantity;
            Console.WriteLine($"  ✓ Updated {product} (Qty: {cart[product].Quantity})");
        }
        else
        {
            cart[product] = new CartItem(product, prices[product], quantity);
            Console.WriteLine($"  ✓ Added {product} to cart");
        }
    }

    static void UpdateQuantity(Dictionary<string, CartItem> cart, string product, int quantity)
    {
        if (cart.ContainsKey(product))
        {
            cart[product].Quantity = quantity;
            Console.WriteLine($"  ✓ {product} quantity updated to {quantity}");
        }
        else
        {
            Console.WriteLine($"  ✗ {product} not in cart");
        }
    }

    static void RemoveFromCart(Dictionary<string, CartItem> cart, string product)
    {
        if (cart.Remove(product))
        {
            Console.WriteLine($"  ✓ {product} removed from cart");
        }
        else
        {
            Console.WriteLine($"  ✗ {product} not in cart");
        }
    }
}
