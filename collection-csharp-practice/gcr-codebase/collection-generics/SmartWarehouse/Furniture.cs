namespace SmartWarehouse
{
    /// <summary>
    /// Furniture class inheriting from WarehouseItem
    /// </summary>
    public class Furniture : WarehouseItem
    {
        public string Material { get; set; }
        public string Style { get; set; }

        public Furniture(string itemId, string name, double price, int quantity, string material, string style)
            : base(itemId, name, price, quantity)
        {
            Material = material;
            Style = style;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[FURNITURE] ID: {ItemId} | Name: {Name} | Material: {Material} | Style: {Style} | Price: ${Price:F2} | Qty: {Quantity}");
        }

        public override decimal CalculateValue()
        {
            return (decimal)(Price * Quantity);
        }
    }
}
