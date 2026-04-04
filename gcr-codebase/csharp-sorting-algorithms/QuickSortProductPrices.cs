using System;

// Quick Sort - Sort Product Prices (ascending)
// Problem: Sort product prices using Quick Sort
class QuickSortProductPrices
{
    static void QuickSort(decimal[] arr, int low, int high)
    {
        if (low < high)
        {
            int p = Partition(arr, low, high);
            QuickSort(arr, low, p - 1);
            QuickSort(arr, p + 1, high);
        }
    }

    static int Partition(decimal[] arr, int low, int high)
    {
        decimal pivot = arr[high]; // choose last element as pivot
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                decimal tmp = arr[i];
                arr[i] = arr[j];
                arr[j] = tmp;
            }
        }
        decimal tmp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = tmp2;
        return i + 1;
    }

    static void Main()
    {
        decimal[] prices = { 49.99m, 299.99m, 19.99m, 99.95m, 199.00m };
        Console.WriteLine("Before: " + string.Join(", ", prices));
        QuickSort(prices, 0, prices.Length - 1);
        Console.WriteLine("After:  " + string.Join(", ", prices));
    }
}
