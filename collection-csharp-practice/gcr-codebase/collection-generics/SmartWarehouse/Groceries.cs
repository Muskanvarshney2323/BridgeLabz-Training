namespace SmartWarehouse
{
    /// <summary>
    /// Groceries class inheriting from WarehouseItem
    /// </summary>
    public class Groceries : WarehouseItem
    {
        public string ExpiryDate { get; set; }
        public bool IsOrganic { get; set; }

        public Groceries(string itemId, string name, double price, int quantity, string expiryDate, bool isOrganic)
            : base(itemId, name, price, quantity)
        {
            ExpiryDate = expiryDate;
            IsOrganic = isOrganic;
        }

        public override void DisplayInfo()
        {
            string organic = IsOrganic ? "Organic" : "Non-Organic";
            Console.WriteLine($"[GROCERIES] ID: {ItemId} | Name: {Name} | {organic} | Price: ${Price:F2} | Qty: {Quantity} | Expires: {ExpiryDate}");
        }

        public override decimal CalculateValue()
        {
            return (decimal)(Price * Quantity);
        }
    }
}
