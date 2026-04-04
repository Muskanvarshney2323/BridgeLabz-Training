using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // E-commerce: Customer places Orders; Order aggregates Products
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    class Order
    {
        public string OrderId { get; set; }
        public List<Product> Items { get; private set; }

        public Order(string id)
        {
            OrderId = id;
            Items = new List<Product>();
        }

        public void AddProduct(Product p)
        {
            Items.Add(p);
        }

        public double Total()
        {
            double total = 0;
            foreach (var p in Items) total += p.Price;
            return total;
        }
    }

    class Customer
    {
        public string Name { get; set; }
        public List<Order> Orders { get; private set; }

        public Customer(string name)
        {
            Name = name;
            Orders = new List<Order>();
        }

        public Order PlaceOrder(string orderId)
        {
            var o = new Order(orderId);
            Orders.Add(o);
            return o;
        }
    }

    class Program
    {
        static void Main()
        {
            var cust = new Customer("Oliver");
            var p1 = new Product("Laptop", 1000);
            var p2 = new Product("Mouse", 20);

            var order = cust.PlaceOrder("ORD1001");
            order.AddProduct(p1);
            order.AddProduct(p2);

            Console.WriteLine($"Order {order.OrderId} total: {order.Total()}");
        }
    }
}