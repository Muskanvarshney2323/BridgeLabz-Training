using System;
class WindChill
{
    static void Main()
    {
        Console.Write("Enter temperature (Fahrenheit): "); double temp = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter wind speed (mph): "); double windSpeed = Convert.ToDouble(Console.ReadLine());
        double wc = CalculateWindChill(temp, windSpeed);
        Console.WriteLine($"Calculated wind chill = {wc:F2}");
    }

    public static double CalculateWindChill(double temperature, double windSpeed)
    {
        return 35.74 + 0.6215 * temperature + (0.4275 * temperature - 35.75) * Math.Pow(windSpeed, 0.16);
    }
}