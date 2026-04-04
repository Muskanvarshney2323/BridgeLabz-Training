using System;
class FactorsOfNumber
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int n = Convert.ToInt32(Console.ReadLine());
        if (n <= 0) { Console.WriteLine("Enter a positive integer."); return; }

        int[] factors = GetFactors(n);
        Console.WriteLine("Factors: " + string.Join(", ", factors));
        Console.WriteLine($"Sum = {SumOfArray(factors)}");
        Console.WriteLine($"Product = {ProductOfArray(factors)}");
        Console.WriteLine($"Sum of squares = {SumOfSquares(factors)}");
    }

    public static int[] GetFactors(int number)
    {
        int absn = Math.Abs(number);
        int count = 0;
        for (int i = 1; i <= absn; i++) if (absn % i == 0) count++;
        int[] result = new int[count];
        int idx = 0;
        for (int i = 1; i <= absn; i++) if (absn % i == 0) result[idx++] = i;
        return result;
    }

    public static long SumOfArray(int[] arr)
    {
        long s = 0; foreach (int v in arr) s += v; return s;
    }

    public static long ProductOfArray(int[] arr)
    {
        long p = 1; foreach (int v in arr) p *= v; return p;
    }

    public static double SumOfSquares(int[] arr)
    {
        double s = 0; foreach (int v in arr) s += Math.Pow(v, 2); return s;
    }
}