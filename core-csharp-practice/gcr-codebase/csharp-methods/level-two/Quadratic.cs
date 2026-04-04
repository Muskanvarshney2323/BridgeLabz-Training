using System;
class Quadratic
{
    static void Main()
    {
        Console.Write("Enter a: "); double a = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter b: "); double b = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter c: "); double c = Convert.ToDouble(Console.ReadLine());

        double[] roots = FindRoots(a, b, c);
        if (roots.Length == 0) Console.WriteLine("No real roots.");
        else if (roots.Length == 1) Console.WriteLine($"One root: {roots[0]}");
        else Console.WriteLine($"Root1 = {roots[0]}, Root2 = {roots[1]}");
    }

    public static double[] FindRoots(double a, double b, double c)
    {
        if (a == 0) return new double[0];
        double delta = b * b - 4 * a * c;
        if (delta < 0) return new double[0];
        if (Math.Abs(delta) < 1e-12) return new double[] { -b / (2 * a) };
        double s = Math.Sqrt(delta);
        return new double[] { (-b + s) / (2 * a), (-b - s) / (2 * a) };
    }
}