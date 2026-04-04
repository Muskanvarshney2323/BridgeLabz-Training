namespace DynamicMarketplace
{
    /// <summary>
    /// Base abstract class for product categories
    /// </summary>
    public abstract class Category
    {
        public string CategoryName { get; set; }

        protected Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
