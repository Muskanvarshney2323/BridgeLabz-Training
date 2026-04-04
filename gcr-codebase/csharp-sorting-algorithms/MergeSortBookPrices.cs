using System;

// Merge Sort - Sort an Array of Book Prices (ascending)
// Problem: Sort a list of book prices using Merge Sort
class MergeSortBookPrices
{
    static void MergeSort(decimal[] arr)
    {
        if (arr.Length <= 1) return;
        int mid = arr.Length / 2;
        decimal[] left = new decimal[mid];
        decimal[] right = new decimal[arr.Length - mid];
        Array.Copy(arr, 0, left, 0, mid);
        Array.Copy(arr, mid, right, 0, arr.Length - mid);
        MergeSort(left);
        MergeSort(right);
        Merge(arr, left, right);
    }

    static void Merge(decimal[] arr, decimal[] left, decimal[] right)
    {
        int i = 0, j = 0, k = 0;
        while (i < left.Length && j < right.Length)
        {
            if (left[i] <= right[j]) arr[k++] = left[i++];
            else arr[k++] = right[j++];
        }
        while (i < left.Length) arr[k++] = left[i++];
        while (j < right.Length) arr[k++] = right[j++];
    }

    static void Main()
    {
        decimal[] prices = { 299.99m, 159.50m, 399.00m, 49.99m, 199.75m };
        Console.WriteLine("Before: " + string.Join(", ", prices));
        MergeSort(prices);
        Console.WriteLine("After:  " + string.Join(", ", prices));
    }
}
