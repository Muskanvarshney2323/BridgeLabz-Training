using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the base of the triangle in inches: ");
        double base_inches = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the height of the triangle in inches: ");
        double height_inches = Convert.ToDouble(Console.ReadLine());

        double area_inches = 0.5 * base_inches * height_inches;
        double area_cm = area_inches * 6.4516; // 1 square inch = 6.4516 square centimeters

        Console.WriteLine("The area of the triangle is " + area_inches + " square inches and " + area_cm + " square centimeters.");
    }
}