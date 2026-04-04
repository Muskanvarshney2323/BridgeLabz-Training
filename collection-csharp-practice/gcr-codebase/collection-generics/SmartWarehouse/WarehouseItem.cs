namespace SmartWarehouse
{
    /// <summary>
    /// Abstract base class for all warehouse items
    /// Demonstrates: Inheritance and polymorphism
    /// </summary>
    public abstract class WarehouseItem
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        protected WarehouseItem(string itemId, string name, double price, int quantity)
        {
            ItemId = itemId;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public abstract void DisplayInfo();
        public abstract decimal CalculateValue();
    }
}
