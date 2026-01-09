using System;
using System.Collections.Generic;

namespace HashMapProblems
{
    /// <summary>
    /// Problem 2: Check for a Pair with Given Sum in an Array
    /// 
    /// Given an array and a target sum, find if there exists a pair of elements whose sum is equal to the target.
    /// 
    /// Hint: Store visited numbers in a hash map and check if target - current_number exists in the map.
    /// 
    /// Time Complexity: O(n) where n is the length of the array
    /// Space Complexity: O(n) for the hash map
    /// </summary>
    public class CheckForPairWithGivenSum
    {
        /// <summary>
        /// Check if a pair with given sum exists in the array
        /// Returns true if pair exists, false otherwise
        /// </summary>
        public static bool PairExists(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                return false;

            HashSet<int> visitedNumbers = new HashSet<int>();

            foreach (int num in nums)
            {
                int complement = target - num;

                // Check if complement exists in the set
                if (visitedNumbers.Contains(complement))
                    return true;

                // Add current number to the set
                visitedNumbers.Add(num);
            }

            return false;
        }

        /// <summary>
        /// Find and return the pair with given sum
        /// Returns a tuple (num1, num2) if pair exists, or (int.MinValue, int.MinValue) if not found
        /// </summary>
        public static (int, int) FindPair(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                return (int.MinValue, int.MinValue);

            HashSet<int> visitedNumbers = new HashSet<int>();

            foreach (int num in nums)
            {
                int complement = target - num;

                if (visitedNumbers.Contains(complement))
                    return (complement, num);

                visitedNumbers.Add(num);
            }

            return (int.MinValue, int.MinValue);
        }

        /// <summary>
        /// Find all pairs with given sum (handles duplicates)
        /// Returns a list of pairs
        /// </summary>
        public static List<(int, int)> FindAllPairs(int[] nums, int target)
        {
            List<(int, int)> result = new List<(int, int)>();

            if (nums == null || nums.Length < 2)
                return result;

            HashSet<int> visited = new HashSet<int>();
            HashSet<int> seen = new HashSet<int>();

            foreach (int num in nums)
            {
                int complement = target - num;

                if (visited.Contains(complement))
                {
                    // Avoid duplicate pairs
                    int smaller = Math.Min(num, complement);
                    int larger = Math.Max(num, complement);
                    string pairKey = smaller + "," + larger;

                    if (!seen.Contains(pairKey))
                    {
                        result.Add((smaller, larger));
                        seen.Add(pairKey);
                    }
                }

                visited.Add(num);
            }

            return result;
        }

        /// <summary>
        /// Find indices of a pair with given sum
        /// Returns a tuple (index1, index2) if pair exists, or (-1, -1) if not found
        /// </summary>
        public static (int, int) FindPairIndices(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                return (-1, -1);

            Dictionary<int, int> numIndexMap = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (numIndexMap.ContainsKey(complement))
                    return (numIndexMap[complement], i);

                if (!numIndexMap.ContainsKey(nums[i]))
                    numIndexMap[nums[i]] = i;
            }

            return (-1, -1);
        }
    }

    // Example Usage
    public class CheckForPairWithGivenSumExample
    {
        public static void Main()
        {
            int[] nums1 = { 2, 7, 11, 15, 3 };
            int target1 = 9;

            Console.WriteLine($"Array: [{string.Join(", ", nums1)}]");
            Console.WriteLine($"Target Sum: {target1}");
            Console.WriteLine($"Pair Exists: {CheckForPairWithGivenSum.PairExists(nums1, target1)}");

            var pair = CheckForPairWithGivenSum.FindPair(nums1, target1);
            if (pair != (int.MinValue, int.MinValue))
            {
                Console.WriteLine($"Pair Found: ({pair.Item1}, {pair.Item2})");
                Console.WriteLine($"Sum: {pair.Item1 + pair.Item2}");
            }

            var indices = CheckForPairWithGivenSum.FindPairIndices(nums1, target1);
            if (indices != (-1, -1))
            {
                Console.WriteLine($"Indices: ({indices.Item1}, {indices.Item2})");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums2 = { 10, 20, 30, 40, 50, 20, 30 };
            int target2 = 50;

            Console.WriteLine($"Array: [{string.Join(", ", nums2)}]");
            Console.WriteLine($"Target Sum: {target2}");

            var allPairs = CheckForPairWithGivenSum.FindAllPairs(nums2, target2);
            Console.WriteLine($"All Unique Pairs with Sum {target2}:");
            if (allPairs.Count > 0)
            {
                foreach (var (num1, num2) in allPairs)
                {
                    Console.WriteLine($"  ({num1}, {num2})");
                }
            }
            else
            {
                Console.WriteLine("  No pairs found");
            }

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums3 = { 5, 10, 15, 20 };
            int target3 = 100;

            Console.WriteLine($"Array: [{string.Join(", ", nums3)}]");
            Console.WriteLine($"Target Sum: {target3}");
            Console.WriteLine($"Pair Exists: {CheckForPairWithGivenSum.PairExists(nums3, target3)}");
        }
    }
}
