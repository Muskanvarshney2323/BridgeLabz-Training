namespace SmartWarehouse
{
    /// <summary>
    /// Electronics class inheriting from WarehouseItem
    /// </summary>
    public class Electronics : WarehouseItem
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }

        public Electronics(string itemId, string name, double price, int quantity, string brand, int warrantyMonths)
            : base(itemId, name, price, quantity)
        {
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[ELECTRONICS] ID: {ItemId} | Name: {Name} | Brand: {Brand} | Price: ${Price:F2} | Qty: {Quantity} | Warranty: {WarrantyMonths} months");
        }

        public override decimal CalculateValue()
        {
            return (decimal)(Price * Quantity);
        }
    }
}
