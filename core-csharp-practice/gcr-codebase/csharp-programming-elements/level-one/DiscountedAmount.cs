using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the Student Fee: ");
        double fee = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the University Discount Percentage: ");
        double discount_percent = Convert.ToDouble(Console.ReadLine());

        double discount = (discount_percent / 100) * fee;
        double discounted_fee = fee - discount;

        Console.WriteLine("The discount amount is INR " + discount + " and final discounted fee is INR " + discounted_fee);
    }
}