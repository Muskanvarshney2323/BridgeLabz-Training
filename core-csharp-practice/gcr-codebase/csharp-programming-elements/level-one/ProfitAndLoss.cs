using System;

class Program
{
    static void Main()
    {
        double cost_price = 129;
        double selling_price = 191;

        double profit = selling_price - cost_price;
        double profitPercentage = (profit / cost_price) * 100;

        Console.WriteLine(
            "The Cost Price is INR " + cost_price + " and Selling Price is INR " + selling_price +
            "\nThe Profit is INR " + profit + " and the Profit Percentage is " + profitPercentage
        );
    }
}
