using System;
using System.Collections.Generic;

namespace SmartCheckout
{
    /// <summary>
    /// Represents a customer with items to purchase.
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<(string ItemName, int Quantity)> Items { get; set; }
        public decimal TotalBill { get; set; }

        public Customer(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Items = new List<(string, int)>();
            TotalBill = 0;
        }

        public void AddItem(string itemName, int quantity)
        {
            Items.Add((itemName, quantity));
        }

        public override string ToString()
        {
            return $"Customer ID: {CustomerId}, Name: {CustomerName}, Items Count: {Items.Count}";
        }
    }
}
