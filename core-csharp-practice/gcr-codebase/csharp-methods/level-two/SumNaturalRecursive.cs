using System;
class SumNaturalRecursive
{
    static void Main()
    {
        Console.Write("Enter a natural number (n >= 1): ");
        int n = Convert.ToInt32(Console.ReadLine());
        if (n < 1) { Console.WriteLine("Not a natural number."); return; }

        int r = SumRecursion(n);
        int f = SumFormula(n);
        Console.WriteLine($"Sum by recursion = {r}");
        Console.WriteLine($"Sum by formula = {f}");
        Console.WriteLine(r == f ? "Both methods match." : "Mismatch detected.");
    }

    public static int SumRecursion(int n)
    {
        if (n == 1) return 1;
        return n + SumRecursion(n - 1);
    }

    public static int SumFormula(int n) => n * (n + 1) / 2;
}