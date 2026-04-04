using System;
class HarshadNo 
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int number = Convert.ToInt32(Console.ReadLine());
        int originalNumber = number; 
        int sum = 0;

        while (number != 0)
        {
            sum += number % 10; 
            number /= 10;      
        }

        if (originalNumber % sum == 0)
        {
            Console.WriteLine($"{originalNumber} is a Harshad Number.");
        }
        else
        {
            Console.WriteLine($"{originalNumber} is Not a Harshad Number.");
        }
    }
}