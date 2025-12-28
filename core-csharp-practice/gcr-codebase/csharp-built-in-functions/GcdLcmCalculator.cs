using System;

class GcdLcmCalculator
{
    static void Main()
    {
        int a = ReadInt("Enter first integer: ");
        int b = ReadInt("Enter second integer: ");
        int g = GCD(Math.Abs(a), Math.Abs(b));
        long l = LCM(a, b);
        Console.WriteLine($"GCD({a}, {b}) = {g}");
        Console.WriteLine($"LCM({a}, {b}) = {l}");
    }

    static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out int v)) Console.Write("Invalid integer. Try again: ");
        return v;
    }

    static int GCD(int x, int y)
    {
        while (y != 0)
        {
            int t = x % y;
            x = y;
            y = t;
        }
        return x;
    }

    static long LCM(int x, int y)
    {
        if (x == 0 || y == 0) return 0;
        return Math.Abs((long)x / GCD(Math.Abs(x), Math.Abs(y)) * y);
    }
}