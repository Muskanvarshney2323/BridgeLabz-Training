using System;

class PrimeNumberChecker
{
    static void Main()
    {
        int n = ReadInt("Enter an integer to check for primality: ");
        bool isPrime = IsPrime(n);
        Console.WriteLine(isPrime ? $"{n} is prime." : $"{n} is NOT prime.");
    }

    static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out int value))
        {
            Console.Write("Invalid integer. Try again: ");
        }
        return value;
    }

    public static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0) return n == 2;
        int r = (int)Math.Sqrt(n);
        for (int i = 3; i <= r; i += 2)
            if (n % i == 0) return false;
        return true;
    }
}