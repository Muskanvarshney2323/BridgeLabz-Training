
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter a natural number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number >= 0)
        {
            // Compute sum using for loop
            int sumForLoop = 0;
            for (int i = 1; i <= number; i++)
            {
                sumForLoop += i;
            }

            // Compute sum using formula
            int sumFormula = number * (number + 1) / 2;

            // Display results
            Console.WriteLine($"Sum using for loop: {sumForLoop}");
            Console.WriteLine($"Sum using formula: {sumFormula}");

            // Compare results
            if (sumForLoop == sumFormula)
            {
                Console.WriteLine("Both computations are correct and yield the same result.");
            }
            else
            {
                Console.WriteLine("There is a discrepancy between the two computations.");
            }
        }
        else
        {
            Console.WriteLine($"The number {number} is not a natural number.");
        }
    }
}