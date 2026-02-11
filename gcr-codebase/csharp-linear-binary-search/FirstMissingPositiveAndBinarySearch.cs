using System;

// Challenge: Find first missing positive (linear time, constant space) and binary search on sorted array
public static class FirstMissingPositiveAndBinarySearch
{
    // Linear algorithm (in-place marking)
    public static int FirstMissingPositive(int[] nums)
    {
        if (nums == null) return 1;
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            while (nums[i] >= 1 && nums[i] <= n && nums[nums[i] - 1] != nums[i])
            {
                int tmp = nums[nums[i] - 1];
                nums[nums[i] - 1] = nums[i];
                nums[i] = tmp;
            }
        }
        for (int i = 0; i < n; i++) if (nums[i] != i + 1) return i + 1;
        return n + 1;
    }

    // Binary search on sorted array to find index of target, or -1 if not found
    public static int BinarySearchIndex(int[] nums, int target)
    {
        if (nums == null) return -1;
        int left = 0, right = nums.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target) return mid;
            if (nums[mid] < target) left = mid + 1;
            else right = mid - 1;
        }
        return -1;
    }

    public static void Main()
    {
        int[] a = {3, 4, -1, 1};
        Console.WriteLine(FirstMissingPositive((int[])a.Clone())); // prints 2

        int[] b = {5,2,4,1,3};
        Array.Sort(b);
        Console.WriteLine(BinarySearchIndex(b, 4)); // prints index of 4 in sorted array
    }
}