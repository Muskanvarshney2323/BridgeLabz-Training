public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Discount { get; set; }
    public Product(int productId, string name, double discount)
    {
        ProductId = productId;
        Name = name;
        Discount = discount;
    }
}