using System;
class TriangularParkRounds
{
    static void Main()
    {
        Console.Write("Enter side A (meters): "); double a = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter side B (meters): "); double b = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter side C (meters): "); double c = Convert.ToDouble(Console.ReadLine());

        int rounds = ComputeRoundsFor5Km(a, b, c);
        Console.WriteLine($"To complete 5 km the athlete must run {rounds} round(s) (perimeter = {a + b + c} m)");
    }

    public static int ComputeRoundsFor5Km(double a, double b, double c)
    {
        double perimeter = a + b + c;
        if (perimeter <= 0) return 0;
        const double target = 5000.0; // meters
        return (int)Math.Ceiling(target / perimeter);
    }
}