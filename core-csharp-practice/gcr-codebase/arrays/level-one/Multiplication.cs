using System;
class Multiplication
{
    static void Main()
    {
        Console.WriteLine("Enter a number to print its multiplication table: ");
        int number = Convert.ToInt32(Console.ReadLine());
        int[] multiplicationTable = new int[10];

        for (int i = 1; i <= 10; i++)
        {
            multiplicationTable[i - 1] = number * i;
        }

        Console.WriteLine("Multiplication table of {0}:", number);
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine("{0} * {1} = {2}", number, i, multiplicationTable[i - 1]);
        }
    }
}