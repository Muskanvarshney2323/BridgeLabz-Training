using System;

public class FlashDealzMenu
{
    private Product[] products;
    private ISorter sorter;

    public FlashDealzMenu()
    {
        products = DataUtility.GetSampleProducts();
        sorter = new QuickSortUtility();
    }

    public void ShowMenu()
    {
        int choice;

        do
        {
            Console.WriteLine("\n---- FlashDealz Menu ----");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Sort Products by Discount");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // ✅ object method call
                    DisplayProducts();
                    break;

                case 2:
                    // ✅ object fields usage
                    sorter.Sort(products);
                    Console.WriteLine("Products sorted by discount successfully!");
                    break;

                case 3:
                    Console.WriteLine("Exiting application...");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

        } while (choice != 3);
    }

    private void DisplayProducts()
    {
        Console.WriteLine("\nID\tName\tDiscount");

        foreach (Product p in products)
        {
            // ❌ NO Price, only Discount
            Console.WriteLine($"{p.ProductId}\t{p.Name}\t\t{p.Discount}%");
        }
    }
}
