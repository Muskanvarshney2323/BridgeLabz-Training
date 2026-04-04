using System;
class MultiplicationSixToNine
{
    static void Main()
    {
        Console.WriteLine("Enter a number to print its multiplication table from 6 to 9: ");
        int number = Convert.ToInt32(Console.ReadLine());
        int[] multiplicationResult = new int[4];

        for (int i = 6; i <= 9; i++)
        {
            multiplicationResult[i - 6] = number * i;
        }

        Console.WriteLine("Multiplication table of {0} from 6 to 9:", number);
        for (int i = 6; i <= 9; i++)
        {
            Console.WriteLine("{0} * {1} = {2}", number, i, multiplicationResult[i - 6]);
        }
    }
}