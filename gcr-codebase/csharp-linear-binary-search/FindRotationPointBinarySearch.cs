using System;

// Find index of smallest element (rotation point) in rotated sorted array using binary search
public static class FindRotationPointBinarySearch
{
    public static int FindRotationPoint(int[] nums)
    {
        if (nums == null || nums.Length == 0) return -1;
        int left = 0, right = nums.Length - 1;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] > nums[right]) left = mid + 1;
            else right = mid;
        }
        return left;
    }

    public static void Main()
    {
        int[] a = { 4,5,6,7,0,1,2 };
        Console.WriteLine(FindRotationPoint(a)); // prints 4
    }
}