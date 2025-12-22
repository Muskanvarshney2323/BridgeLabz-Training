 using System;
class Program   
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number >= 0)
        {
            long factorial = 1;

            for (int count = 1; count <= number; count++)
            {
                factorial *= count;
            }

            Console.WriteLine($"The factorial of {number} is {factorial}");
        }
        else
        {
            Console.WriteLine("Please enter a positive integer.");
        }
    }
}