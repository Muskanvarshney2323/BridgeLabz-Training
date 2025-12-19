using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter side 1 of the triangular park in meters: ");
        double side1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter side 2 of the triangular park in meters: ");
        double side2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter side 3 of the triangular park in meters: ");
        double side3 = Convert.ToDouble(Console.ReadLine());

        double perimeter = side1 + side2 + side3; // Calculate the perimeter of the triangle
        double totalDistance = 5000; // Total distance to run in meters (5 km)
        double rounds = totalDistance / perimeter; // Calculate the number of rounds

        Console.WriteLine("The total number of rounds the athlete will run is " + rounds + " to complete 5 km");
    }
}