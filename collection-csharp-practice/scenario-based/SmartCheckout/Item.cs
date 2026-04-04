namespace SmartCheckout
{
    /// <summary>
    /// Represents an item in the supermarket inventory.
    /// </summary>
    public class Item
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Item(string itemName, decimal price, int quantity)
        {
            ItemName = itemName;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Item: {ItemName}, Price: Rs.{Price}, Stock: {Quantity}";
        }
    }
}
