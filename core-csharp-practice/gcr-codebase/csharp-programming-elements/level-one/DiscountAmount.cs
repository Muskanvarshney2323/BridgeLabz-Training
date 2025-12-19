using System;
class Program
{
    static void Main()
    {
        double fee = 125000;
        double discount_percent = 10;

        double discount = (discount_percent / 100) * fee;
        double discounted_fee = fee - discount;

        Console.WriteLine("The discount amount is INR " + discount + " and final discounted fee is INR " + discounted_fee);
    }
}