using System;
class SumOfNaturalNumbersLoop
{
    static void Main()
    {
        Console.Write("Enter n (non-negative integer): ");
        int n = Convert.ToInt32(Console.ReadLine());
        if (n < 0) { Console.WriteLine("Invalid input."); return; }
        Console.WriteLine($"Sum of first {n} natural numbers is {SumOfNatural(n)}");
    }

    public static int SumOfNatural(int n)
    {
        int sum = 0;
        for (int i = 1; i <= n; i++) sum += i;
        return sum;
    }
}