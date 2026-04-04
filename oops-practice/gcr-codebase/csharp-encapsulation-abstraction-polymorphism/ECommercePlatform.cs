using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface ITaxable
    {
        double CalculateTax();
        string GetTaxDetails();
    }

    abstract class Product
    {
        private int _productId;
        private string _name;
        private double _price;

        protected Product(int id, string name, double price)
        {
            _productId = id; _name = name; _price = price;
        }

        public int ProductId => _productId;
        public string Name => _name;
        public double Price => _price;

        public abstract double CalculateDiscount();
    }

    class Electronics : Product, ITaxable
    {
        public Electronics(int id, string name, double price) : base(id, name, price) { }
        public override double CalculateDiscount() => Price * 0.05;
        public double CalculateTax() => Price * 0.18;
        public string GetTaxDetails() => "GST 18%";
    }

    class Clothing : Product, ITaxable
    {
        public Clothing(int id, string name, double price) : base(id, name, price) { }
        public override double CalculateDiscount() => Price * 0.10;
        public double CalculateTax() => Price * 0.05;
        public string GetTaxDetails() => "GST 5%";
    }

    class Groceries : Product
    {
        public Groceries(int id, string name, double price) : base(id, name, price) { }
        public override double CalculateDiscount() => 0;
    }

    class Program
    {
        static void Main()
        {
            var products = new List<Product>
            {
                new Electronics(1, "Phone", 20000),
                new Clothing(2, "Shirt", 1200),
                new Groceries(3, "Rice", 500)
            };

            foreach (var p in products)
            {
                double tax = (p is ITaxable t) ? t.CalculateTax() : 0;
                double discount = p.CalculateDiscount();
                double final = p.Price + tax - discount;
                Console.WriteLine($"{p.Name}: Price {p.Price:C}, Tax {tax:C}, Discount {discount:C} => Final {final:C}");
            }
        }
    }
}