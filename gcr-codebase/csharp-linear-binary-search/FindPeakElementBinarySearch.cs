using System;

// Find a peak element (element greater than neighbors) using binary search
public static class FindPeakElementBinarySearch
{
    public static int FindPeak(int[] nums)
    {
        if (nums == null || nums.Length == 0) return -1;
        int left = 0, right = nums.Length - 1;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] > nums[mid + 1]) right = mid;
            else left = mid + 1;
        }
        return left;
    }

    public static void Main()
    {
        int[] a = { 1, 2, 3, 1 };
        Console.WriteLine(FindPeak(a)); // prints 2 (value 3)
    }
}