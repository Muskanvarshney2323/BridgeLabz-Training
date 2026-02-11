using System;
using System.Collections.Generic;

namespace HashMapProblems
{

    public class CheckForPairWithGivenSum
    {
      
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
