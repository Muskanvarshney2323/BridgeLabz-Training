using System;

// Selection Sort - Sort Exam Scores (ascending)
// Problem: Sort students' exam scores using Selection Sort
class SelectionSortExamScores
{
    static void SelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIdx]) minIdx = j;
            }
            int tmp = arr[minIdx];
            arr[minIdx] = arr[i];
            arr[i] = tmp;
        }
    }

    static void Main()
    {
        int[] scores = { 78, 92, 67, 88, 74, 95 };
        Console.WriteLine("Before: " + string.Join(", ", scores));
        SelectionSort(scores);
        Console.WriteLine("After:  " + string.Join(", ", scores));
    }
}
