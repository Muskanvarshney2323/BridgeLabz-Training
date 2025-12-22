using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number % 5 == 0)
        {
            Console.WriteLine("Is the number " + number + " divisible by 5? Yes");
        }
        else
        {
            Console.WriteLine("Is the number " + number + " divisible by 5? No");
        }
    }
}