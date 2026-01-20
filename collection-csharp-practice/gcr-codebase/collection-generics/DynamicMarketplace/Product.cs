namespace DynamicMarketplace
{
    /// <summary>
    /// Generic Product class with constraint T : Category
    /// Demonstrates: Type Parameters, Generic Classes, Constraints
    /// </summary>
    public class Product<T> where T : Category
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double OriginalPrice { get; set; }
        public double CurrentPrice { get; set; }
        public int StockQuantity { get; set; }
        public T CategoryInfo { get; set; }

        public Product(string productId, string productName, double price, int stock, T categoryInfo)
        {
            ProductId = productId;
            ProductName = productName;
            OriginalPrice = price;
            CurrentPrice = price;
            StockQuantity = stock;
            CategoryInfo = categoryInfo;
        }

        public void DisplayProductInfo()
        {
            double discount = ((OriginalPrice - CurrentPrice) / OriginalPrice) * 100;
            Console.WriteLine($"[{CategoryInfo.CategoryName}] ID: {ProductId} | Name: {ProductName} | Original: ${OriginalPrice:F2} | Current: ${CurrentPrice:F2} | Discount: {discount:F1}% | Stock: {StockQuantity}");
        }

        public override string ToString()
        {
            return $"{ProductName} ({CategoryInfo.CategoryName}) - ${CurrentPrice:F2}";
        }
    }
}
