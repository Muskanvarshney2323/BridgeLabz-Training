using System;

// Bubble Sort - Sort Student Marks (ascending)
// Problem: Sort an array of student marks using Bubble Sort
class BubbleSortStudentMarks
{
    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                    swapped = true;
                }
            }
            if (!swapped) break; // already sorted
        }
    }

    static void Main()
    {
        int[] marks = { 85, 72, 90, 66, 92, 74 };
        Console.WriteLine("Before: " + string.Join(", ", marks));
        BubbleSort(marks);
        Console.WriteLine("After:  " + string.Join(", ", marks));
    }
}
