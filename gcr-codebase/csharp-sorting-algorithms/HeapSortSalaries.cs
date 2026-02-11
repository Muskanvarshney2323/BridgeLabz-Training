using System;

// Heap Sort - Sort Job Applicants by Salary (ascending)
// Problem: Sort salary demands using Heap Sort
class HeapSortSalaries
{
    static void HeapSort(decimal[] arr)
    {
        int n = arr.Length;
        // Build max heap
        for (int i = n / 2 - 1; i >= 0; i--) Heapify(arr, n, i);
        // One by one extract elements
        for (int i = n - 1; i > 0; i--)
        {
            decimal tmp = arr[0]; arr[0] = arr[i]; arr[i] = tmp;
            Heapify(arr, i, 0);
        }
    }

    static void Heapify(decimal[] arr, int heapSize, int root)
    {
        int largest = root;
        int left = 2 * root + 1;
        int right = 2 * root + 2;
        if (left < heapSize && arr[left] > arr[largest]) largest = left;
        if (right < heapSize && arr[right] > arr[largest]) largest = right;
        if (largest != root)
        {
            decimal tmp = arr[root]; arr[root] = arr[largest]; arr[largest] = tmp;
            Heapify(arr, heapSize, largest);
        }
    }

    static void Main()
    {
        decimal[] salaries = { 50000m, 75000m, 60000m, 45000m, 90000m };
        Console.WriteLine("Before: " + string.Join(", ", salaries));
        HeapSort(salaries);
        Console.WriteLine("After:  " + string.Join(", ", salaries));
    }
}
