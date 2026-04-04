using System;
class OddEvenArrays 
{
    static void Main()
    {
        Console.WriteLine("Enter a natural number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number < 1)
        {
            Console.WriteLine("Error: Please enter a natural number greater than 0.");
            return;
        }

        int evenSize = number / 2 + 1;
        int oddSize = number / 2 + (number % 2 == 0 ? 0 : 1);
        int[] evenNumbers = new int[evenSize];
        int[] oddNumbers = new int[oddSize];
        int evenIndex = 0;
        int oddIndex = 0;

        for (int i = 1; i <= number; i++)
        {
            if (i % 2 == 0)
            {
                evenNumbers[evenIndex] = i;
                evenIndex++;
            }
            else
            {
                oddNumbers[oddIndex] = i;
                oddIndex++;
            }
        }

        Console.WriteLine("Even numbers:");
        for (int i = 0; i < evenIndex; i++)
        {
            Console.Write(evenNumbers[i] + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Odd numbers:");
        for (int i = 0; i < oddIndex; i++)
        {
            Console.Write(oddNumbers[i] + " ");
        }
        Console.WriteLine();
    }
}