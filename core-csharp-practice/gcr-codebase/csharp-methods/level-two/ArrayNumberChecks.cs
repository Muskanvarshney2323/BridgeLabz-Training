using System;
class ArrayNumberChecks
{
    static void Main()
    {
        int[] nums = new int[5];
        for (int i = 0; i < 5; i++) { Console.Write($"Enter number #{i+1}: "); nums[i] = Convert.ToInt32(Console.ReadLine()); }

        for (int i = 0; i < 5; i++)
        {
            int n = nums[i];
            int sign = CheckSign(n);
            if (sign > 0) Console.WriteLine($"{n} is positive and {(IsEven(n) ? "even" : "odd")}");
            else if (sign < 0) Console.WriteLine($"{n} is negative");
            else Console.WriteLine($"{n} is zero");
        }

        int comp = Compare(nums[0], nums[4]);
        string desc = comp == 1 ? "greater" : (comp == 0 ? "equal" : "less");
        Console.WriteLine($"First element ({nums[0]}) is {desc} than last element ({nums[4]})");
    }

    static int CheckSign(int n) { if (n > 0) return 1; if (n < 0) return -1; return 0; }
    static bool IsEven(int n) => n % 2 == 0;
    static int Compare(int number1, int number2) { if (number1 > number2) return 1; if (number1 == number2) return 0; return -1; }
}