namespace DynamicMarketplace
{
    /// <summary>
    /// ClothingCategory class
    /// </summary>
    public class ClothingCategory : Category
    {
        public string Size { get; set; }
        public string Color { get; set; }

        public ClothingCategory(string size, string color) : base("Clothing")
        {
            Size = size;
            Color = color;
        }
    }
}
