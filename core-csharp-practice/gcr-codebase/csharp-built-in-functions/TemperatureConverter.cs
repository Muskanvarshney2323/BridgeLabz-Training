using System;

class TemperatureConverter
{
    static void Main()
    {
        Console.WriteLine("Choose conversion: 1) Celsius -> Fahrenheit  2) Fahrenheit -> Celsius");
        Console.Write("Enter choice (1 or 2): ");
        string? choice = Console.ReadLine();
        if (choice == "1")
        {
            double c = ReadDouble("Enter temperature in Celsius: ");
            Console.WriteLine($"{c} °C = {CelsiusToFahrenheit(c):F2} °F");
        }
        else if (choice == "2")
        {
            double f = ReadDouble("Enter temperature in Fahrenheit: ");
            Console.WriteLine($"{f} °F = {FahrenheitToCelsius(f):F2} °C");
        }
        else Console.WriteLine("Invalid choice.");
    }

    static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        while (!double.TryParse(Console.ReadLine(), out double v)) Console.Write("Invalid number. Try again: ");
        return v;
    }

    static double CelsiusToFahrenheit(double c) => (c * 9.0 / 5.0) + 32.0;
    static double FahrenheitToCelsius(double f) => (f - 32.0) * 5.0 / 9.0;
}