using System;

class Program
{
    public static void Main(String [] args)
    {
        double celsius = 25;
        double fahrenheit;

        fahrenheit = (celsius * 9 / 5) + 32;

        Console.WriteLine("Temperature in Fahrenheit = " + fahrenheit);
    }
}
