
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter the age: ");
        int age = Convert.ToInt32(Console.ReadLine());

        if (age >= 18)
        {
            Console.WriteLine($"The person's age is {age} and can vote.");
        }
        else
        {
            Console.WriteLine($"The person's age is {age} and cannot vote.");
        }
    }
}