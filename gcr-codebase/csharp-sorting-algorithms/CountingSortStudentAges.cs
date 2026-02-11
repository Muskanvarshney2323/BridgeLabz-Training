using System;

// Counting Sort - Sort Student Ages (ages 10 to 18 inclusive)
// Problem: Sort students' ages using Counting Sort
class CountingSortStudentAges
{
    static int[] CountingSort(int[] arr, int minValue, int maxValue)
    {
        int range = maxValue - minValue + 1;
        int[] count = new int[range];
        foreach (int val in arr) count[val - minValue]++;
        // cumulative counts (optional for stable sort)
        for (int i = 1; i < range; i++) count[i] += count[i - 1];
        int[] output = new int[arr.Length];
        // place elements in output array in reverse for stability
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int val = arr[i];
            output[--count[val - minValue]] = val;
        }
        return output;
    }

    static void Main()
    {
        int[] ages = { 15, 12, 18, 14, 13, 12, 17, 10 };
        Console.WriteLine("Before: " + string.Join(", ", ages));
        int[] sorted = CountingSort(ages, 10, 18);
        Console.WriteLine("After:  " + string.Join(", ", sorted));
    }
}
