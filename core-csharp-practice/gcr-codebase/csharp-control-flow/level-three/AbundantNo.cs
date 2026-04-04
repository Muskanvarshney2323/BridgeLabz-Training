using System;
class AbundantNo    
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int number = Convert.ToInt32(Console.ReadLine());
        int sum = 0;

        for (int i = 1; i < number; i++)
        {
            if (number % i == 0)
            {
                sum += i;
            }
        }

        if (sum > number)
        {
            Console.WriteLine($"{number} is an Abundant Number.");
        }
        else
        {
            Console.WriteLine($"{number} is Not an Abundant Number.");
        }
    }
}