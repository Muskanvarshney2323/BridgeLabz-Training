using System; 
class Program
{
    static void Main()
    {
        double radius_km = 6378;
        double pi = 3.141592653589793;

        double volume_km3 = (4.0 / 3.0) * pi * Math.Pow(radius_km, 3);
        double km_to_miles_conversion_factor = 1.60934;
        double radius_miles = radius_km / km_to_miles_conversion_factor;
        double volume_miles3 = (4.0 / 3.0) * pi * Math.Pow(radius_miles, 3);

        Console.WriteLine("The volume of earth in cubic kilometers is " + volume_km3 + " and cubic miles is " + volume_miles3);
    }
}