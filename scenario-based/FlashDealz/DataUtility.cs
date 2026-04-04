using System;
class DataUtility
{
    public static Product[] GetSampleProducts()
    {
        Product[] products = new Product[5];

        products[0] = new Product(101, "Shoes  ", 40);
        products[1] = new Product(102, "Watch  ", 25);
        products[2] = new Product(103, "Headphones", 60);
        products[3] = new Product(104, "Backpack", 30);
        products[4] = new Product(105, "Jacket ", 50);
        return products;
    }
}
