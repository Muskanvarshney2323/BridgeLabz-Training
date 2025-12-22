using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter an integer to find its multiplication table from 6 to 9: ");
        int number = Convert.ToInt32(Console.ReadLine());

        for (int i = 6; i <= 9; i++)
        {
            int result = number * i;
            Console.WriteLine($"{number} * {i} = {result}");
        }
    }
}