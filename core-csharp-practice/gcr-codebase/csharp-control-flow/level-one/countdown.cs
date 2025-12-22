using System;
class Program   
{
    static void Main()
    {
        Console.Write("Enter the countdown start number: ");
        int startNumber = Convert.ToInt32(Console.ReadLine());

        for (int counter = startNumber; counter >= 1; counter--)
        {
            Console.WriteLine(counter);
        }

        Console.WriteLine("Rocket Launched!");
    }
}