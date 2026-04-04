using System;

namespace SmartCheckout
{
    /// <summary>
    /// SmartCheckout - Supermarket Billing Queue System
    /// Demonstrates Queue (for customer queue) and HashMap (for inventory)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║     SMART CHECKOUT - SUPERMARKET BILLING SYSTEM    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

            // Initialize Inventory (HashMap)
            Inventory inventory = new Inventory();
            Console.WriteLine("=== Setting up Inventory ===");
            inventory.AddItem("Milk", 60, 50);
            inventory.AddItem("Bread", 40, 30);
            inventory.AddItem("Eggs", 120, 20);
            inventory.AddItem("Butter", 250, 15);
            inventory.AddItem("Rice", 80, 40);
            Console.WriteLine("✓ Inventory initialized with 5 items\n");
            inventory.DisplayInventory();

            // Create Checkout Counter with Queue
            CheckoutCounter counter1 = new CheckoutCounter(1, inventory);

            // Create Customers
            Console.WriteLine("\n=== Creating Customers ===");

            Customer customer1 = new Customer(101, "Rajesh Kumar");
            customer1.AddItem("Milk", 2);
            customer1.AddItem("Bread", 1);
            customer1.AddItem("Eggs", 1);
            Console.WriteLine("✓ Customer 1 created: Rajesh Kumar");

            Customer customer2 = new Customer(102, "Priya Singh");
            customer2.AddItem("Butter", 1);
            customer2.AddItem("Rice", 3);
            customer2.AddItem("Milk", 1);
            Console.WriteLine("✓ Customer 2 created: Priya Singh");

            Customer customer3 = new Customer(103, "Amit Patel");
            customer3.AddItem("Bread", 2);
            customer3.AddItem("Eggs", 2);
            customer3.AddItem("Rice", 2);
            Console.WriteLine("✓ Customer 3 created: Amit Patel");

            // Add customers to queue
            Console.WriteLine("\n=== Adding Customers to Queue ===");
            counter1.AddCustomer(customer1);
            counter1.AddCustomer(customer2);
            counter1.AddCustomer(customer3);

            // Display queue
            counter1.DisplayQueue();

            // Process customers
            Console.WriteLine("\n=== Processing Queue ===");
            while (!counter1.IsQueueEmpty())
            {
                counter1.ProcessNextCustomer();
            }

            // Display final inventory
            inventory.DisplayInventory();

            // Demonstrate error handling - insufficient stock
            Console.WriteLine("\n=== Testing Error Handling ===");
            Customer customer4 = new Customer(104, "Test Customer");
            customer4.AddItem("Milk", 100); // More than available
            customer4.AddItem("InvalidItem", 1); // Item doesn't exist
            counter1.AddCustomer(customer4);
            counter1.ProcessNextCustomer();

            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║          CHECKOUT PROCESS COMPLETED                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
        }
    }
}
