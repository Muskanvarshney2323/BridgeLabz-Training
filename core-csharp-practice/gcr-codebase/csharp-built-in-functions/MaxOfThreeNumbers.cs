using System;

class MaxOfThreeNumbers
{
    static void Main()
    {
        int a = ReadInt("Enter first integer: ");
        int b = ReadInt("Enter second integer: ");
        int c = ReadInt("Enter third integer: ");
        int max = MaxOfThree(a, b, c);
        Console.WriteLine($"Maximum of the three numbers is: {max}");
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

    static int MaxOfThree(int x, int y, int z)
    {
        int m = x;
        if (y > m) m = y;
        if (z > m) m = z;
        return m;
    }
}