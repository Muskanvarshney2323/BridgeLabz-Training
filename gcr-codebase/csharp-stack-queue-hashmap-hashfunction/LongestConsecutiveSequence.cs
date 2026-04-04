using System;
using System.Collections.Generic;
using System.Linq;

namespace HashMapProblems
{

    public class LongestConsecutiveSequence
    {
             public static int LongestConsecutive(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            HashSet<int> numSet = new HashSet<int>(nums);
            int maxLength = 0;

            foreach (int num in numSet)
            {
                // Only start counting from a number if it's the beginning of a sequence
                // i.e., num-1 should not exist in the set
                if (!numSet.Contains(num - 1))
                {
                    int currentNum = num;
                    int currentLength = 1;

                    // Count consecutive numbers
                    while (numSet.Contains(currentNum + 1))
                    {
                        currentNum++;
                        currentLength++;
                    }

                    maxLength = Math.Max(maxLength, currentLength);
                }
            }

            return maxLength;
        }

        /// <summary>
        /// Find the actual longest consecutive sequence
        /// Returns the sequence itself, not just the length
        /// </summary>
        public static List<int> LongestConsecutiveSequenceList(int[] nums)
        {
            List<int> result = new List<int>();

            if (nums == null || nums.Length == 0)
                return result;

            HashSet<int> numSet = new HashSet<int>(nums);
            int maxLength = 0;
            int startNum = 0;

            foreach (int num in numSet)
            {
                // Only start counting from a number if it's the beginning of a sequence
                if (!numSet.Contains(num - 1))
                {
                    int currentNum = num;
                    int currentLength = 1;

                    // Count consecutive numbers
                    while (numSet.Contains(currentNum + 1))
                    {
                        currentNum++;
                        currentLength++;
                    }

                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        startNum = num;
                    }
                }
            }

            // Build the result sequence
            for (int i = 0; i < maxLength; i++)
            {
                result.Add(startNum + i);
            }

            return result;
        }

        /// <summary>
        /// Find all consecutive sequences in the array
        /// </summary>
        public static List<List<int>> FindAllConsecutiveSequences(int[] nums)
        {
            List<List<int>> result = new List<List<int>>();

            if (nums == null || nums.Length == 0)
                return result;

            HashSet<int> numSet = new HashSet<int>(nums);
            HashSet<int> visited = new HashSet<int>();

            foreach (int num in numSet)
            {
                if (!visited.Contains(num))
                {
                    // Only start counting from a number if it's the beginning of a sequence
                    if (!numSet.Contains(num - 1))
                    {
                        List<int> sequence = new List<int>();
                        int currentNum = num;

                        while (numSet.Contains(currentNum))
                        {
                            sequence.Add(currentNum);
                            visited.Add(currentNum);
                            currentNum++;
                        }

                        if (sequence.Count > 1)
                        {
                            result.Add(sequence);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Alternative approach using Union-Find (Disjoint Set Union)
        /// More complex but useful for related problems
        /// </summary>
        public static int LongestConsecutiveUnionFind(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            Dictionary<int, int> parent = new Dictionary<int, int>();
            Dictionary<int, int> rank = new Dictionary<int, int>();

            void MakeSet(int x)
            {
                if (!parent.ContainsKey(x))
                {
                    parent[x] = x;
                    rank[x] = 0;
                }
            }

            int Find(int x)
            {
                if (parent[x] != x)
                    parent[x] = Find(parent[x]);
                return parent[x];
            }

            void Union(int x, int y)
            {
                int px = Find(x);
                int py = Find(y);

                if (px == py) return;

                if (rank[px] < rank[py])
                {
                    parent[px] = py;
                }
                else if (rank[px] > rank[py])
                {
                    parent[py] = px;
                }
                else
                {
                    parent[py] = px;
                    rank[px]++;
                }
            }

            foreach (int num in nums)
            {
                MakeSet(num);
            }

            HashSet<int> numSet = new HashSet<int>(nums);
            foreach (int num in numSet)
            {
                if (numSet.Contains(num + 1))
                {
                    Union(num, num + 1);
                }
            }

            int maxLength = 0;
            Dictionary<int, int> componentSize = new Dictionary<int, int>();

            foreach (int num in numSet)
            {
                int root = Find(num);
                if (!componentSize.ContainsKey(root))
                    componentSize[root] = 0;
                componentSize[root]++;
            }

            maxLength = componentSize.Values.Max();
            return maxLength;
        }
    }

    // Example Usage
    public class LongestConsecutiveSequenceExample
    {
        public static void Main()
        {
            int[] nums1 = { 100, 4, 200, 1, 3, 2 };
            Console.WriteLine($"Array: [{string.Join(", ", nums1)}]");
            Console.WriteLine($"Length of longest consecutive sequence: {LongestConsecutiveSequence.LongestConsecutive(nums1)}");
            Console.WriteLine($"Sequence: [{string.Join(", ", LongestConsecutiveSequence.LongestConsecutiveSequenceList(nums1))}]");

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums2 = { 9, 1, 4, 7, 3,2, 8, 5, 6 };
            Console.WriteLine($"Array: [{string.Join(", ", nums2)}]");
            Console.WriteLine($"Length of longest consecutive sequence: {LongestConsecutiveSequence.LongestConsecutive(nums2)}");
            Console.WriteLine($"Sequence: [{string.Join(", ", LongestConsecutiveSequence.LongestConsecutiveSequenceList(nums2))}]");

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            int[] nums3 = { 0, 3, 7, 2, 5, 8, 4, 6, 9, 1 };
            Console.WriteLine($"Array: [{string.Join(", ", nums3)}]");
            var allSequences = LongestConsecutiveSequence.FindAllConsecutiveSequences(nums3);
            Console.WriteLine("All consecutive sequences:");
            foreach (var seq in allSequences)
            {
                Console.WriteLine($"  [{string.Join(", ", seq)}]");
            }
        }
    }
}
