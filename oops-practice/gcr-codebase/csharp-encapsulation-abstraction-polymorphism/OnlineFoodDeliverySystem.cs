using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface IDiscountable
    {
        void ApplyDiscount(double percent);
        string GetDiscountDetails();
    }

    abstract class FoodItem
    {
        private string _itemName;
        protected double Price;
        protected int Quantity;

        protected FoodItem(string name, double price, int qty)
        {
            _itemName = name; Price = price; Quantity = qty;
        }

        public string ItemName => _itemName;
        public abstract double CalculateTotalPrice();
        public void GetItemDetails() => Console.WriteLine($"{ItemName} x{Quantity} @ {Price:C}");
    }

    class VegItem : FoodItem, IDiscountable
    {
        private double _discountPercent;
        public VegItem(string name, double price, int q) : base(name, price, q) { }
        public override double CalculateTotalPrice() => Price * Quantity * (1 - _discountPercent / 100.0);
        public void ApplyDiscount(double percent) => _discountPercent = percent;
        public string GetDiscountDetails() => $"{_discountPercent}% off";
    }

    class NonVegItem : FoodItem
    {
        public NonVegItem(string name, double price, int q) : base(name, price, q) { }
        public override double CalculateTotalPrice() => Price * Quantity + 30; // packaging
    }

    class Program
    {
        static void Main()
        {
            var items = new List<FoodItem> { new VegItem("Paneer",150,2), new NonVegItem("Chicken",250,1)};
            ((VegItem)items[0]).ApplyDiscount(10);
            foreach(var it in items)
            {
                it.GetItemDetails();
                Console.WriteLine($" Total: {it.CalculateTotalPrice():C}");
            }
        }
    }
}