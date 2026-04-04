using System;
class FactorsOfNumber
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

        int maxFactor = 10;
        int[] factors = new int[maxFactor];
        int index = 0;

        for (int i = 1; i <= number; i++)
        {
            if (number % i == 0)
            {
                if (index == maxFactor)
                {
                    maxFactor *= 2;
                    int[] temp = new int[maxFactor];
                    for (int j = 0; j < factors.Length; j++)
                    {
                        temp[j] = factors[j];
                    }
                    factors = temp;
                }
                factors[index] = i;
                index++;
            }
        }

        Console.WriteLine("Factors of {0} are:", number);
        for (int i = 0; i < index; i++)
        {
            Console.Write(factors[i] + " ");
        }
        Console.WriteLine();
    }
}