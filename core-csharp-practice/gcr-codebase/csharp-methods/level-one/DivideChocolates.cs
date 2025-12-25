using System;
class DivideChocolates
{
    static void Main()
    {
        Console.Write("Enter number of chocolates: "); int chocolates = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter number of children: "); int children = Convert.ToInt32(Console.ReadLine());
        int[] result = FindRemainderAndQuotientMethod(chocolates, children);
        Console.WriteLine($"Each child gets {result[0]} chocolates and {result[1]} will remain");
    }

    public static int[] FindRemainderAndQuotientMethod(int number, int divisor)
    {
        if (divisor <= 0) return new int[] { 0, number };
        int quotient = number / divisor;
        int remainder = number % divisor;
        return new int[] { quotient, remainder };
    }
}