using System;
using System.Diagnostics;

class SortingPerformanceTest
{
    static void Main(string[] args)
    {
        int[] inputSizes = { 1000, 10000 };

        foreach (int count in inputSizes)
        {
            Console.WriteLine($"\nInput Size: {count}");

            int[] baseArray = CreateRandomData(count);
            int[] bubbleArr = (int[])baseArray.Clone();
            int[] mergeArr = (int[])baseArray.Clone();
            int[] quickArr = (int[])baseArray.Clone();

            Console.WriteLine("Bubble Sort Duration: " + TimeBubbleSort(bubbleArr) + " ms");
            Console.WriteLine("Merge Sort Duration: " + TimeMergeSort(mergeArr) + " ms");
            Console.WriteLine("Quick Sort Duration: " + TimeQuickSort(quickArr) + " ms");
        }
    }

    static int[] CreateRandomData(int size)
    {
        int[] values = new int[size];
        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            values[i] = random.Next(1, size);
        }
        return values;
    }

    // ---------- Time Wrappers ----------
    static long TimeBubbleSort(int[] arr)
    {
        Stopwatch watch = Stopwatch.StartNew();
        PerformBubbleSort(arr);
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }

    static long TimeMergeSort(int[] arr)
    {
        Stopwatch watch = Stopwatch.StartNew();
        PerformMergeSort(arr, 0, arr.Length - 1);
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }

    static long TimeQuickSort(int[] arr)
    {
        Stopwatch watch = Stopwatch.StartNew();
        PerformQuickSort(arr, 0, arr.Length - 1);
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }

    // ---------- Bubble Sort ----------
    static void PerformBubbleSort(int[] arr)
    {
        for (int pass = 0; pass < arr.Length - 1; pass++)
        {
            for (int idx = 0; idx < arr.Length - pass - 1; idx++)
            {
                if (arr[idx] > arr[idx + 1])
                {
                    (arr[idx], arr[idx + 1]) = (arr[idx + 1], arr[idx]);
                }
            }
        }
    }

    // ---------- Merge Sort ----------
    static void PerformMergeSort(int[] arr, int start, int end)
    {
        if (start >= end) return;

        int middle = start + (end - start) / 2;
        PerformMergeSort(arr, start, middle);
        PerformMergeSort(arr, middle + 1, end);
        Combine(arr, start, middle, end);
    }

    static void Combine(int[] arr, int start, int mid, int end)
    {
        int leftSize = mid - start + 1;
        int rightSize = end - mid;

        int[] leftArr = new int[leftSize];
        int[] rightArr = new int[rightSize];

        Array.Copy(arr, start, leftArr, 0, leftSize);
        Array.Copy(arr, mid + 1, rightArr, 0, rightSize);

        int i = 0, j = 0, k = start;

        while (i < leftSize && j < rightSize)
        {
            arr[k++] = leftArr[i] <= rightArr[j] ? leftArr[i++] : rightArr[j++];
        }

        while (i < leftSize) arr[k++] = leftArr[i++];
        while (j < rightSize) arr[k++] = rightArr[j++];
    }

    // ---------- Quick Sort ----------
    static void PerformQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotPos = GetPivotIndex(arr, low, high);
            PerformQuickSort(arr, low, pivotPos - 1);
            PerformQuickSort(arr, pivotPos + 1, high);
        }
    }

    static int GetPivotIndex(int[] arr, int low, int high)
    {
        int pivotValue = arr[high];
        int boundary = low - 1;

        for (int i = low; i < high; i++)
        {
            if (arr[i] < pivotValue)
            {
                boundary++;
                (arr[boundary], arr[i]) = (arr[i], arr[boundary]);
            }
        }

        (arr[boundary + 1], arr[high]) = (arr[high], arr[boundary + 1]);
        return boundary + 1;
    }
}
