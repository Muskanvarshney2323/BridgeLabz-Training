using System;
class BonusCalculator
{
    static void Main()
    {
        Console.Write("Enter the salary: ");
        double salary = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the years of service: ");
        int yearsOfService = Convert.ToInt32(Console.ReadLine());

        if (yearsOfService > 5)
        {
            double bonus = salary * 0.05;
            Console.WriteLine($"The bonus amount is: {bonus}");
        }
        else
        {
            Console.WriteLine("No bonus awarded.");
        }
    }
}