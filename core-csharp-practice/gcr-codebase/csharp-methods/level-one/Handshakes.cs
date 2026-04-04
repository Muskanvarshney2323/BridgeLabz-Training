using System;
class Handshakes
{
    static void Main()
    {
        Console.Write("Enter number of students: ");
        int n = Convert.ToInt32(Console.ReadLine());
        if (n < 0) { Console.WriteLine("Invalid number."); return; }
        Console.WriteLine($"Maximum number of handshakes among {n} students is {CalculateHandshakes(n)}");
    }

    public static long CalculateHandshakes(int n) => (long)n * (n - 1) / 2;
}