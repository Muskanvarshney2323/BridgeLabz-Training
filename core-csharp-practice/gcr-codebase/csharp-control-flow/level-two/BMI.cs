
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter weight in kg: ");
        double weight = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter height in cm: ");
        double heightCm = Convert.ToDouble(Console.ReadLine());
        double heightM = heightCm / 100; // Convert cm to meters

        double bmi = weight / (heightM * heightM);
        Console.WriteLine($"Your BMI is: {bmi:F2}");

        if (bmi < 18.5)
        {
            Console.WriteLine("Weight Status: Underweight");
        }
        else if (bmi >= 18.5 && bmi < 24.9)
        {
            Console.WriteLine("Weight Status: Normal weight");
        }
        else if (bmi >= 25 && bmi < 39.9)
        {
            Console.WriteLine("Weight Status: Overweight");
        }
        else
        {
            Console.WriteLine("Weight Status: Obesity");
        }
    }
}