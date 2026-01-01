using System;

class MathUtility
{
    public int Factorial(int n)
    {
        if (n < 0)
            return -1;

        int fact = 1;
        for (int i = 1; i <= n; i++)
        {
            fact = fact * i;
        }
        return fact;
    }

    public bool IsPrime(int n)
    {
        if (n <= 1)
            return false;

        for (int i = 2; i <= n / 2; i++)
        {
            if (n % i == 0)
                return false;
        }
        return true;
    }

    public int GCD(int a, int b)
    {
        if (a < 0) a = -a;
        if (b < 0) b = -b;

        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public int Fibonacci(int n)
    {
        if (n < 0)
            return -1;

        if (n == 0) return 0;
        if (n == 1) return 1;

        int a = 0, b = 1, c = 0;
        for (int i = 2; i <= n; i++)
        {
            c = a + b;
            a = b;
            b = c;
        }
        return c;
    }
}

class Program
{
    static void Main()
    {
        MathUtility math = new MathUtility();

        Console.WriteLine("Enter a number for Factorial:");
        int factNum = Convert.ToInt32(Console.ReadLine());
        int factResult = math.Factorial(factNum);
        Console.WriteLine("Factorial: " + factResult);

        Console.WriteLine("\nEnter a number to check Prime:");
        int primeNum = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Is Prime: " + math.IsPrime(primeNum));

        Console.WriteLine("\nEnter first number for GCD:");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter second number for GCD:");
        int b = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("GCD: " + math.GCD(a, b));

        Console.WriteLine("\nEnter value of n for Fibonacci:");
        int fibNum = Convert.ToInt32(Console.ReadLine());
        int fibResult = math.Fibonacci(fibNum);
        Console.WriteLine("Fibonacci number: " + fibResult);
    }
}
