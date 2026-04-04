using System;
class FindRemainderAndQuotient
{
    static void Main()
    {
        Console.Write("Enter dividend (integer): "); int number = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter divisor (integer): "); int divisor = Convert.ToInt32(Console.ReadLine());
        int[] qr = FindRemainderAndQuotientMethod(number, divisor);
        Console.WriteLine($"Quotient = {qr[0]}, Remainder = {qr[1]}");
    }

    public static int[] FindRemainderAndQuotientMethod(int number, int divisor)
    {
        if (divisor == 0) throw new DivideByZeroException("Divisor cannot be zero.");
        int quotient = number / divisor;
        int remainder = number % divisor;
        return new int[] { quotient, remainder };
    }
}