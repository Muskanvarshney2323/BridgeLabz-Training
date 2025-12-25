using System;
class SimpleInterestCalculator
{
    static void Main()
    {
        Console.WriteLine("Enter the Principal amount: ");
        double principal = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter the Rate of Interest: ");
        double rate = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter the Time (in years): ");
        double time = Convert.ToDouble(Console.ReadLine());

        double simpleInterest = CalculateSimpleInterest(principal, rate, time);

        Console.WriteLine($"The Simple Interest is {simpleInterest} for Principal {principal}, Rate of Interest {rate} and Time {time}");
    }

    static double CalculateSimpleInterest(double principal, double rate, double time)
    {
        return (principal * rate * time) / 100;
    }
}