using System;
class Product
{
    // instance variables
    string productName;
    double price;

    // class variable
    static int totalProducts = 0;
    public Product(string n, double p)
    {
        this.productName = n;
        this.price = p;
        totalProducts++;
    }
    public void ProductDetails()
    {
        Console.WriteLine("Product Name: " + productName);
        Console.WriteLine("Product Price: " + price);
    }
    public void TotalProducts()
    {
        Console.WriteLine("Total Products: " + totalProducts);
    }
    public static void Main()
    {
        Product prod1 = new Product("Laptop", 75000.00);
        prod1.ProductDetails();
        prod1.TotalProducts();

        Console.WriteLine();

        Product prod2 = new Product("Smartphone", 50000.00);
        prod2.ProductDetails();
        prod2.TotalProducts();
    }
}