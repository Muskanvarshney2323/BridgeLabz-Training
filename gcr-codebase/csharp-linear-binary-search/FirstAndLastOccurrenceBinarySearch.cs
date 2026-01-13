using System;

// Find first and last occurrence of target in sorted array using binary search
public static class FirstAndLastOccurrenceBinarySearch
{
    public static int[] SearchRange(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return new[] { -1, -1 };
        int first = FindBound(nums, target, true);
        int last = FindBound(nums, target, false);
        return new[] { first, last };
    }

    private static int FindBound(int[] nums, int target, bool isFirst)
    {
        int left = 0, right = nums.Length - 1, bound = -1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                bound = mid;
                if (isFirst) right = mid - 1; else left = mid + 1;
            }
            else if (nums[mid] < target) left = mid + 1;
            else right = mid - 1;
        }
        return bound;
    }

    public static void Main()
    {
        int[] a = {5,7,7,8,8,10};
        var r = SearchRange(a, 8);
        Console.WriteLine($"[{r[0]}, {r[1]}]"); // prints [3, 4]
    }
}