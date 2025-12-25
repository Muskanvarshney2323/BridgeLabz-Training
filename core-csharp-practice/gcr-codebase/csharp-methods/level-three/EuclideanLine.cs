using System;
class EuclideanLine
{
    static void Main()
    {
        Console.Write("x1: "); double x1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("y1: "); double y1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("x2: "); double x2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("y2: "); double y2 = Convert.ToDouble(Console.ReadLine());

        double dist = Distance(x1, y1, x2, y2);
        Console.WriteLine($"Euclidean distance = {dist:F4}");

        double[] line = LineEquation(x1, y1, x2, y2);
        if (double.IsNaN(line[0])) Console.WriteLine("Line is vertical: x = " + line[1]);
        else Console.WriteLine($"Line: y = {line[0]:F4}x + {line[1]:F4}");
    }

    public static double Distance(double x1, double y1, double x2, double y2)
        => Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

    // returns [slope m, intercept b]. If vertical line, slope = NaN and b = x (x = constant)
    public static double[] LineEquation(double x1, double y1, double x2, double y2)
    {
        if (Math.Abs(x2 - x1) < 1e-12) return new double[] { double.NaN, x1 };
        double m = (y2 - y1) / (x2 - x1);
        double b = y1 - m * x1;
        return new double[] { m, b };
    }
}