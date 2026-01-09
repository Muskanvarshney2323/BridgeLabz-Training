using System;
using System.Collections.Generic;

namespace HashMapProblems
{
    /// <summary>
    /// Problem 5: Two Sum Problem
    /// 
    /// Given an array and a target sum, find two indices such that their values add up to the target.
    /// 
    /// Hint: Use a hash map to store the index of each element as you iterate. 
    /// Check if target - current_element exists in the map.
    /// 
    /// Time Complexity: O(n) where n is the length of the array
    /// Space Complexity: O(n) for the hash map
    /// </summary>
    public class TwoSumProblem
    {
        /// <summary>
        /// Find two indices whose values add up to target
        /// Returns an array of two indices [i, j] where i < j
        /// Throws exception if no such pair exists
        /// </summary>
        public static int[] TwoSum(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new ArgumentException("Array must have at least 2 elements");

            Dictionary<int, int> numIndexMap = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                // Check if complement was seen before
                if (numIndexMap.ContainsKey(complement))
                {
                    return new int[] { numIndexMap[complement], i };
                }

                // Store the current number and its index (skip if duplicate)
                if (!numIndexMap.ContainsKey(nums[i]))
                {
                    numIndexMap[nums[i]] = i;
                }
            }

            throw new InvalidOperationException($"No two sum solution for target {target}");
        }

        /// <summary>
        /// Try to find two indices without throwing exception
        /// Returns true if pair found, false otherwise
        /// </summary>
        public static bool TryTwoSum(int[] nums, int target, out int[] result)
        {
            result = null;

            if (nums == null || nums.Length < 2)
                return false;

            Dictionary<int, int> numIndexMap = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (numIndexMap.ContainsKey(complement))
                {
                    result = new int[] { numIndexMap[complement], i };
                    return true;
                }

                if (!numIndexMap.ContainsKey(nums[i]))
                {
                    numIndexMap[nums[i]] = i;
                }
            }

            return false;
        }

        /// <summary>
        /// Find all pairs that sum to target (with their indices)
        /// Handles duplicate values in the array
        /// </summary>
        public static List<(int index1, int index2)> FindAllTwoSumPairs(int[] nums, int target)
        {
            List<(int, int)> result = new List<(int, int)>();

            if (nums == null || nums.Length < 2)
                return result;

            Dictionary<int, List<int>> numIndexMap = new Dictionary<int, List<int>>();

            // Build map of value -> list of indices
            for (int i = 0; i < nums.Length; i++)
            {
                if (!numIndexMap.ContainsKey(nums[i]))
                {
                    numIndexMap[nums[i]] = new List<int>();
                }
                numIndexMap[nums[i]].Add(i);
            }

            // Find all pairs
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (numIndexMap.ContainsKey(complement))
                {
                    foreach (int j in numIndexMap[complement])
                    {
                        if (i < j)
                        {
                            result.Add((i, j));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find the pair with given sum (sorted array approach)
        /// More efficient for sorted arrays
        /// </summary>
        public static (int, int) TwoSumSorted(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                return (-1, -1);

            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                int sum = nums[left] + nums[right];

                if (sum == target)
                {
                    return (left, right);
                }
                else if (sum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return (-1, -1);
        }

        /// <summary>
        /// Find all unique pairs that sum to target
        /// Returns pairs of values (not indices), without duplicates
        /// </summary>
        public static List<(int, int)> FindUniqueValuePairs(int[] nums, int target)
        {
            List<(int, int)> result = new List<(int, int)>();

            if (nums == null || nums.Length < 2)
                return result;

            HashSet<int> seen = new HashSet<int>();
            HashSet<string> pairs = new HashSet<string>(); // To avoid duplicate pairs

            foreach (int num in nums)
            {
                int complement = target - num;

                if (seen.Contains(complement))
                {
                    // Create a unique key for the pair
                    int smaller = Math.Min(num, complement);
                    int larger = Math.Max(num, complement);
                    string pairKey = $"{smaller},{larger}";

                    if (!pairs.Contains(pairKey))
                    {
                        result.Add((smaller, larger));
                        pairs.Add(pairKey);
                    }
                }

                seen.Add(num);
            }

            return result;
        }

        /// <summary>
        /// Get closest pair sum to target
        /// </summary>
        public static (int index1, int index2, int sum) GetClosestTwoSum(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new ArgumentException("Array must have at least 2 elements");

            int closestDiff = int.MaxValue;
            int index1 = -1, index2 = -1;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int sum = nums[i] + nums[j];
                    int diff = Math.Abs(sum - target);

                    if (diff < closestDiff)
                    {
                        closestDiff = diff;
                        index1 = i;
                        index2 = j;
                    }
                }
            }

            return (index1, index2, nums[index1] + nums[index2]);
        }
    }

    // Example Usage
    public class TwoSumProblemExample
    {
        public static void Main()
        {
            int[] nums1 = { 2, 7, 11, 15 };
            int target1 = 9;

            Console.WriteLine($"Array: [{string.Join(", ", nums1)}]");
            Console.WriteLine($"Target: {target1}");

            try
            {
                int[] indices = TwoSumProblem.TwoSum(nums1, target1);
                Console.WriteLine($"Indices: [{indices[0]}, {indices[1]}]");
                Console.WriteLine($"Values: [{nums1[indices[0]]}, {nums1[indices[1]]}]");
                Console.WriteLine($"Sum: {nums1[indices[0]] + nums1[indices[1]]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums2 = { 3, 2, 4, 1, 5 };
            int target2 = 6;

            Console.WriteLine($"Array: [{string.Join(", ", nums2)}]");
            Console.WriteLine($"Target: {target2}");

            if (TwoSumProblem.TryTwoSum(nums2, target2, out int[] result))
            {
                Console.WriteLine($"Indices: [{result[0]}, {result[1]}]");
                Console.WriteLine($"Values: [{nums2[result[0]]}, {nums2[result[1]]}]");
            }
            else
            {
                Console.WriteLine("No pair found");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums3 = { 1, 5, 7, -1, 5 };
            int target3 = 6;

            Console.WriteLine($"Array: [{string.Join(", ", nums3)}]");
            Console.WriteLine($"Target: {target3}");
            Console.WriteLine("All pairs (index1, index2):");

            var allPairs = TwoSumProblem.FindAllTwoSumPairs(nums3, target3);
            foreach (var (i, j) in allPairs)
            {
                Console.WriteLine($"  ({i}, {j}): [{nums3[i]}, {nums3[j]}]");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums4 = { 3, 2, 5, 4, 1 };
            int target4 = 7;

            Console.WriteLine($"Array: [{string.Join(", ", nums4)}]");
            Console.WriteLine($"Target: {target4}");
            Console.WriteLine("Unique value pairs:");

            var uniquePairs = TwoSumProblem.FindUniqueValuePairs(nums4, target4);
            foreach (var (val1, val2) in uniquePairs)
            {
                Console.WriteLine($"  ({val1}, {val2})");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums5 = { 1, 5, 7, 10 };
            int target5 = 15;

            Console.WriteLine($"Array: [{string.Join(", ", nums5)}]");
            Console.WriteLine($"Target: {target5}");

            var (idx1, idx2, sum) = TwoSumProblem.GetClosestTwoSum(nums5, target5);
            Console.WriteLine($"Closest pair indices: ({idx1}, {idx2})");
            Console.WriteLine($"Values: [{nums5[idx1]}, {nums5[idx2]}]");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Distance from target: {Math.Abs(sum - target5)}");
        }
    }
}
