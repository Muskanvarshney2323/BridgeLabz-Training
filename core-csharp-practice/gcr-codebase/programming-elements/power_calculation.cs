using System;

class Program
{
    static void Main()
    {
        double baseNum = 2;
        double exponent = 3;

        double result = Math.Pow(baseNum, exponent);

        Console.WriteLine(baseNum + " raised to the power " + exponent + " = " + result);
    }
}
