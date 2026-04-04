using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the distance in feet: ");
        double distanceInFeet = Convert.ToDouble(Console.ReadLine());

        double distanceInYards = distanceInFeet / 3; // 1 yard = 3 feet
        double distanceInMiles = distanceInYards / 1760; // 1 mile = 1760 yards

        Console.WriteLine("The distance " + distanceInFeet + " feet is " + distanceInYards + " yards and " + distanceInMiles + " miles.");
    }
}