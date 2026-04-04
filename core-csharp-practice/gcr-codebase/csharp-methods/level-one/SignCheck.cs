using System;
class SignCheck
{
    static void Main()
    {
        Console.Write("Enter an integer: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int r = CheckSign(n);
        string text = r == 1 ? "positive" : (r == -1 ? "negative" : "zero");
        Console.WriteLine($"The number {n} is {text}");
    }

    public static int CheckSign(int n)
    {
        if (n > 0) return 1;
        if (n < 0) return -1;
        return 0;
    }
}