using System;

// Search target in 2D matrix where each row is sorted ascending and first element of each row is greater than last of previous row
public static class SearchIn2DSortedMatrix
{
    public static bool SearchMatrix(int[][] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0) return false;
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int left = 0, right = rows * cols - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int r = mid / cols;
            int c = mid % cols;
            if (matrix[r][c] == target) return true;
            if (matrix[r][c] < target) left = mid + 1;
            else right = mid - 1;
        }
        return false;
    }

    public static void Main()
    {
        int[][] m = new int[][] {
            new int[] {1, 3, 5, 7},
            new int[] {10, 11, 16, 20},
            new int[] {23, 30, 34, 60}
        };
        Console.WriteLine(SearchMatrix(m, 3)); // prints True
    }
}