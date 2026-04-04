using System;

class BasicCalculator
{
    static void Main()
    {
        Console.WriteLine("Basic Calculator — choose operation: +, -, *, /");
        Console.Write("Enter operation (+ - * /): ");
        string? op = Console.ReadLine();
        double a = ReadDouble("Enter first number: ");
        double b = ReadDouble("Enter second number: ");

        switch (op)
        {
            case "+": Console.WriteLine($"Result: {Add(a, b)}"); break;
            case "-": Console.WriteLine($"Result: {Subtract(a, b)}"); break;
            case "*": Console.WriteLine($"Result: {Multiply(a, b)}"); break;
            case "/":
                try { Console.WriteLine($"Result: {Divide(a, b)}"); }
                catch (DivideByZeroException ex) { Console.WriteLine(ex.Message); }
                break;
            default: Console.WriteLine("Invalid operation."); break;
        }
    }

    static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        while (!double.TryParse(Console.ReadLine(), out double v)) Console.Write("Invalid number. Try again: ");
        return v;
    }

    static double Add(double x, double y) => x + y;
    static double Subtract(double x, double y) => x - y;
    static double Multiply(double x, double y) => x * y;
    static double Divide(double x, double y)
    {
        if (y == 0) throw new DivideByZeroException("Cannot divide by zero.");
        return x / y;
    }
}