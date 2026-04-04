
using System;
class Program   
{
    static void Main()
    {
        Console.Write("Enter the countdown start number: ");
        int counter = Convert.ToInt32(Console.ReadLine());

        while (counter >= 1)
        {
            Console.WriteLine(counter);
            counter--;
        }

        Console.WriteLine("Rocket Launched!");
    }
}