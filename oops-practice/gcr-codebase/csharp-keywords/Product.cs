using System;

namespace csharp_keywords
{
    class Product
    {
        public static double Discount = 5.0; // percent

        public readonly string ProductID;
        public string ProductName;
        public double Price;
        public int Quantity;

        public Product(string ProductName, double Price, int Quantity, string ProductID)
        {
            this.ProductName = ProductName;
            this.Price = Price;
            this.Quantity = Quantity;
            this.ProductID = ProductID; // readonly
        }

        public static void UpdateDiscount(double newDiscount)
        {
            Discount = newDiscount;
        }

        public void DisplayDetails(object obj)
        {
            if (obj is Product p)
            {
                Console.WriteLine("--- Product ---");
                Console.WriteLine($"Name: {p.ProductName}");
                Console.WriteLine($"ID: {p.ProductID}");
                Console.WriteLine($"Price: {p.Price}");
                Console.WriteLine($"Quantity: {p.Quantity}");
                Console.WriteLine($"Current Discount: {Discount}%");
            }
            else
            {
                Console.WriteLine("Object is not a Product instance.");
            }
        }

        static void Main()
        {
            var prod = new Product("Laptop", 750.0, 2, "PRD-1001");
            prod.DisplayDetails(prod);
            Product.UpdateDiscount(10);
            Console.WriteLine("Updated Discount: " + Product.Discount + "%");
        }
    }
}