using System;
using System.Diagnostics;

class SearchPerformanceTest
{
    static void Main(string[] args)
    {
        int[] dataSizes = { 1000, 10000, 1000000 };

        foreach (int length in dataSizes)
        {
            Console.WriteLine($"\nArray Length: {length}");

            int[] numbers = GenerateSortedArray(length);
            int key = length - 1;

            long linearTime = MeasureLinearSearch(numbers, key);
            Console.WriteLine("Linear Search Duration: " + linearTime + " ms");

            long binaryTime = MeasureBinarySearch(numbers, key);
            Console.WriteLine("Binary Search Duration: " + binaryTime + " ms");
        }
    }

    static int[] GenerateSortedArray(int size)
    {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = i;
        }
        return arr;
    }

    static long MeasureLinearSearch(int[] arr, int value)
    {
        Stopwatch timer = Stopwatch.StartNew();
        PerformLinearSearch(arr, value);
        timer.Stop();
        return timer.ElapsedMilliseconds;
    }

    static long MeasureBinarySearch(int[] arr, int value)
    {
        Stopwatch timer = Stopwatch.StartNew();
        PerformBinarySearch(arr, value);
        timer.Stop();
        return timer.ElapsedMilliseconds;
    }

    static int PerformLinearSearch(int[] arr, int value)
    {
        foreach (int item in arr)
        {
            if (item == value)
                return item;
        }
        return -1;
    }

    static int PerformBinarySearch(int[] arr, int value)
    {
        int start = 0;
        int end = arr.Length - 1;

        while (start <= end)
        {
            int middle = start + (end - start) / 2;

            if (arr[middle] == value)
                return middle;
            else if (arr[middle] < value)
                start = middle + 1;
            else
                end = middle - 1;
        }
        return -1;
    }
}
