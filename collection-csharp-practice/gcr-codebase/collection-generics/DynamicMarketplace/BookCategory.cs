namespace DynamicMarketplace
{
    /// <summary>
    /// BookCategory class
    /// </summary>
    public class BookCategory : Category
    {
        public string Author { get; set; }
        public string Genre { get; set; }

        public BookCategory(string author, string genre) : base("Books")
        {
            Author = author;
            Genre = genre;
        }
    }
}
