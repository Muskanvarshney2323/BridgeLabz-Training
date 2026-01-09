using System;
using System.Collections.Generic;

namespace HashMapProblems
{
    public class FindAllSubarraysWithZeroSum
    {
        /// <summary>
        /// Find all subarrays with zero sum and return their indices
        /// Returns a list of tuples containing (start index, end index) of each zero-sum subarray
        /// </summary>
        public static List<(int start, int end)> FindZeroSumSubarrays(int[] nums)
        {
            List<(int start, int end)> result = new List<(int start, int end)>();
            
            if (nums == null || nums.Length == 0)
                return result;

            // Dictionary to store cumulative sum and list of indices where it occurs
            Dictionary<int, List<int>> sumIndexMap = new Dictionary<int, List<int>>();
            sumIndexMap[0] = new List<int> { -1 }; // Initialize with 0 sum at index -1

            int cumulativeSum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                cumulativeSum += nums[i];

                // If this cumulative sum was seen before, subarrays between those indices have zero sum
                if (sumIndexMap.ContainsKey(cumulativeSum))
                {
                    foreach (int prevIndex in sumIndexMap[cumulativeSum])
                    {
                        result.Add((prevIndex + 1, i));
                    }

                    sumIndexMap[cumulativeSum].Add(i);
                }
                else
                {
                    sumIndexMap[cumulativeSum] = new List<int> { i };
                }
            }

            return result;
        }

        /// <summary>
        /// Find count of all subarrays with zero sum
        /// </summary>
        public static int CountZeroSumSubarrays(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            Dictionary<int, int> sumFrequency = new Dictionary<int, int>();
            sumFrequency[0] = 1; // Empty prefix has sum 0

            int cumulativeSum = 0;
            int count = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                cumulativeSum += nums[i];

                // If this sum was seen before, it means there's a zero-sum subarray
                if (sumFrequency.ContainsKey(cumulativeSum))
                {
                    count += sumFrequency[cumulativeSum];
                    sumFrequency[cumulativeSum]++;
                }
                else
                {
                    sumFrequency[cumulativeSum] = 1;
                }
            }

            return count;
        }
    }

    // Example Usage
    public class FindAllSubarraysWithZeroSumExample
    {
        public static void Main()
        {
            int[] nums1 = { 15, -2, 2, -8, 1, 7, -10, 13 };
            List<(int start, int end)> subarrays = FindAllSubarraysWithZeroSum.FindZeroSumSubarrays(nums1);

            Console.WriteLine($"Array: [{string.Join(", ", nums1)}]");
            Console.WriteLine($"\nZero-sum subarrays (start, end):");
            if (subarrays.Count > 0)
            {
                foreach (var (start, end) in subarrays)
                {
                    Console.WriteLine($"  [{start}, {end}] -> Elements: [{string.Join(", ", nums1[start..(end + 1)])}]");
                }
            }
            else
            {
                Console.WriteLine("  No zero-sum subarrays found");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums2 = { 1, -1, 3, 2, -2, -3 };
            int count = FindAllSubarraysWithZeroSum.CountZeroSumSubarrays(nums2);
            Console.WriteLine($"Array: [{string.Join(", ", nums2)}]");
            Console.WriteLine($"Count of zero-sum subarrays: {count}");
        }
    }
}
