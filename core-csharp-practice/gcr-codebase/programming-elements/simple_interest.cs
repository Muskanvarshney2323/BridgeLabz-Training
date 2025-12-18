using System;

class Program
{
    static void Main()
    {
        double principal = 10000;
        double rate = 5; // in %
        double time = 2; // in years

        double simpleInterest = (principal * rate * time) / 100;

        Console.WriteLine("Simple Interest = " + simpleInterest);
    }
}
