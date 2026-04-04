using System;

// Insertion Sort - Sort Employee IDs (ascending)
// Problem: Sort an unsorted array of employee IDs using Insertion Sort
class InsertionSortEmployeeIDs
{
    static void InsertionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
    }

    static void Main()
    {
        int[] employeeIds = { 5023, 1024, 3301, 2048, 4100 };
        Console.WriteLine("Before: " + string.Join(", ", employeeIds));
        InsertionSort(employeeIds);
        Console.WriteLine("After:  " + string.Join(", ", employeeIds));
    }
}
