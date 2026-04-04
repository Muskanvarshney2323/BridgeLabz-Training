using System;
using System.Collections.Generic;

namespace SmartCheckout
{
    /// <summary>
    /// Manages a checkout counter with a Queue of customers.
    /// </summary>
    public class CheckoutCounter
    {
        private Queue<Customer> customerQueue;
        private Inventory inventory;
        private int counterId;

        public CheckoutCounter(int counterId, Inventory inventory)
        {
            this.counterId = counterId;
            this.inventory = inventory;
            customerQueue = new Queue<Customer>();
        }

        /// <summary>
        /// Add a customer to the queue.
        /// </summary>
        public void AddCustomer(Customer customer)
        {
            customerQueue.Enqueue(customer);
            Console.WriteLine($"✓ Counter {counterId}: Customer '{customer.CustomerName}' added to queue. Queue length: {customerQueue.Count}");
        }

        /// <summary>
        /// Process the next customer in the queue.
        /// </summary>
        public void ProcessNextCustomer()
        {
            if (customerQueue.Count == 0)
            {
                Console.WriteLine($"✗ Counter {counterId}: No customers in queue!");
                return;
            }

            Customer customer = customerQueue.Dequeue();
            Console.WriteLine($"\n--- Processing Counter {counterId} ---");
            Console.WriteLine($"Customer: {customer.CustomerName} (ID: {customer.CustomerId})");

            decimal totalBill = 0;

            foreach (var (itemName, quantity) in customer.Items)
            {
                try
                {
                    if (!inventory.ItemExists(itemName))
                    {
                        Console.WriteLine($"  ✗ Item '{itemName}' not found in inventory!");
                        continue;
                    }

                    int availableStock = inventory.GetStock(itemName);
                    if (availableStock < quantity)
                    {
                        Console.WriteLine($"  ✗ Item '{itemName}': Only {availableStock} available, {quantity} requested!");
                        continue;
                    }

                    decimal price = inventory.GetPrice(itemName);
                    decimal itemTotal = price * quantity;
                    totalBill += itemTotal;

                    // Update stock
                    inventory.DeductStock(itemName, quantity);
                    Console.WriteLine($"  ✓ {itemName}: {quantity} x Rs.{price} = Rs.{itemTotal}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"  ✗ Error: {ex.Message}");
                }
            }

            customer.TotalBill = totalBill;
            Console.WriteLine($"\n📦 TOTAL BILL: Rs.{totalBill}");
            Console.WriteLine($"Queue remaining: {customerQueue.Count}");
        }

        /// <summary>
        /// Get the number of customers waiting in queue.
        /// </summary>
        public int GetQueueLength()
        {
            return customerQueue.Count;
        }

        /// <summary>
        /// Check if queue is empty.
        /// </summary>
        public bool IsQueueEmpty()
        {
            return customerQueue.Count == 0;
        }

        /// <summary>
        /// Display all customers in the queue.
        /// </summary>
        public void DisplayQueue()
        {
            if (customerQueue.Count == 0)
            {
                Console.WriteLine($"Counter {counterId}: Queue is empty!");
                return;
            }

            Console.WriteLine($"\n--- Counter {counterId} Queue ---");
            int position = 1;
            foreach (var customer in customerQueue)
            {
                Console.WriteLine($"{position}. {customer}");
                position++;
            }
        }
    }
}
