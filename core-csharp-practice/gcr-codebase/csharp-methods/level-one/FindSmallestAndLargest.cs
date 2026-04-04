using System;
class FindSmallestAndLargest
{
    static void Main()
    {
        Console.Write("Enter number 1: "); int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter number 2: "); int b = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter number 3: "); int c = Convert.ToInt32(Console.ReadLine());

        int[] res = FindSmallestAndLargestMethod(a, b, c);
        Console.WriteLine($"Smallest = {res[0]}, Largest = {res[1]}");
    }

    public static int[] FindSmallestAndLargestMethod(int number1, int number2, int number3)
    {
        int smallest = Math.Min(number1, Math.Min(number2, number3));
        int largest = Math.Max(number1, Math.Max(number2, number3));
        return new int[] { smallest, largest };
    }
}