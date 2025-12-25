using System;
class TrigonometricFunctions
{
    static void Main()
    {
        Console.Write("Enter angle in degrees: "); double angle = Convert.ToDouble(Console.ReadLine());
        double[] results = CalculateTrigonometricFunctions(angle);
        Console.WriteLine($"sin({angle}) = {results[0]:F4}");
        Console.WriteLine($"cos({angle}) = {results[1]:F4}");
        Console.WriteLine($"tan({angle}) = {results[2]:F4}");
    }

    public static double[] CalculateTrigonometricFunctions(double angle)
    {
        double radians = angle * Math.PI / 180.0;
        double s = Math.Sin(radians);
        double c = Math.Cos(radians);
        double t = Math.Tan(radians);
        return new double[] { s, c, t };
    }
}