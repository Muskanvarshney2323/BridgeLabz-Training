using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the weight in pounds: ");
        double weight_pounds = Convert.ToDouble(Console.ReadLine());

        double weight_kg = weight_pounds / 2.2;

        Console.WriteLine("The weight of the person in pounds is " + weight_pounds + " and in kg is " + weight_kg);
    }
}