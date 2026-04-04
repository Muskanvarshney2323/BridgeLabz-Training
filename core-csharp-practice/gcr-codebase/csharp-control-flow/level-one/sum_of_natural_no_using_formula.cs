
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter a natural number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number >= 0)
        {
            // Compute sum using while loop
            int sumWhileLoop = 0;
            int count = 1;
            while (count <= number)
            {
                sumWhileLoop += count;
                count++;
            }

            // Compute sum using formula
            int sumFormula = number * (number + 1) / 2;

            // Display results
            Console.WriteLine($"Sum using while loop: {sumWhileLoop}");
            Console.WriteLine($"Sum using formula: {sumFormula}");

            // Compare results
            if (sumWhileLoop == sumFormula)
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