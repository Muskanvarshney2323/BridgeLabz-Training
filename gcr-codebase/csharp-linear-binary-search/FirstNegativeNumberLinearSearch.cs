using System;

// Linear search to find the first negative number in an integer array
public static class FirstNegativeNumberLinearSearch
{
    public static int IndexOfFirstNegative(int[] arr)
    {
        if (arr == null) return -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < 0) return i;
        }
        return -1; // not found
    }

    public static void Main()
    {
        int[] a = { 3, 2, 0, -1, -5 };
        Console.WriteLine(IndexOfFirstNegative(a)); // prints 3
    }
}