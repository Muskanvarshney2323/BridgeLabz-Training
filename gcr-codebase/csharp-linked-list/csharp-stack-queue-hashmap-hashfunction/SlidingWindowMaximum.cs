using System;
using System.Collections.Generic;
using System.Linq;

namespace StackQueueProblems
{
    /// <summary>
    /// Problem 4: Sliding Window Maximum
    /// 
    /// Given an array and a window size k, find the maximum element in each sliding window of size k.
    /// 
    /// Hint: Use a deque (double-ended queue) to maintain indices of useful elements in each window.
    /// 
    /// Time Complexity: O(n) where n is the length of the array
    /// Space Complexity: O(k) where k is the window size
    /// </summary>
    public class SlidingWindowMaximum
    {
        /// <summary>
        /// Find the maximum element in each sliding window of size k
        /// 
        /// Example: nums = [1,3,-1,-3,5,3,6,7], k = 3
        /// Output: [3,3,5,5,6,7]
        /// </summary>
        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || k <= 0 || k > nums.Length)
                return new int[0];

            int n = nums.Length;
            int[] result = new int[n - k + 1];
            
            // Using LinkedList to implement deque functionality
            LinkedList<int> deque = new LinkedList<int>(); // Stores indices

            for (int i = 0; i < n; i++)
            {
                // Remove indices that are out of current window (outside the k-sized window)
                if (deque.Count > 0 && deque.First.Value < i - k + 1)
                {
                    deque.RemoveFirst();
                }

                // Remove indices of elements smaller than current element from the back
                // We keep only useful elements in deque
                while (deque.Count > 0 && nums[deque.Last.Value] < nums[i])
                {
                    deque.RemoveLast();
                }

                // Add current index to deque
                deque.AddLast(i);

                // The front of deque contains the index of maximum element for this window
                if (i >= k - 1)
                {
                    result[i - k + 1] = nums[deque.First.Value];
                }
            }

            return result;
        }

        /// <summary>
        /// Alternative approach using a priority queue (MaxHeap)
        /// Less efficient but more straightforward
        /// </summary>
        public static int[] MaxSlidingWindowHeap(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || k <= 0)
                return new int[0];

            int n = nums.Length;
            int[] result = new int[n - k + 1];
            
            // Priority queue: store (value, index) and sort by value in descending order
            var maxHeap = new PriorityQueue<(int, int), int>((a, b) => b.CompareTo(a));

            for (int i = 0; i < k; i++)
            {
                maxHeap.Enqueue((nums[i], i), -nums[i]); // Negative for max heap
            }

            result[0] = nums[maxHeap.Peek().Item1];

            for (int i = k; i < n; i++)
            {
                // Remove elements outside the window
                while (maxHeap.Count > 0 && maxHeap.Peek().Item2 <= i - k)
                {
                    maxHeap.Dequeue();
                }

                maxHeap.Enqueue((nums[i], i), -nums[i]);
                result[i - k + 1] = nums[maxHeap.Peek().Item1];
            }

            return result;
        }
    }

    // Example Usage
    public class SlidingWindowMaximumExample
    {
        public static void Main()
        {
            int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 };
            int k = 3;

            int[] result = SlidingWindowMaximum.MaxSlidingWindow(nums, k);

            Console.WriteLine($"Array: [{string.Join(", ", nums)}]");
            Console.WriteLine($"Window Size: {k}");
            Console.WriteLine($"Maximum in each window: [{string.Join(", ", result)}]");

            Console.WriteLine("\nExplanation:");
            Console.WriteLine("Window [1, 3, -1] -> Maximum: 3");
            Console.WriteLine("Window [3, -1, -3] -> Maximum: 3");
            Console.WriteLine("Window [-1, -3, 5] -> Maximum: 5");
            Console.WriteLine("Window [-3, 5, 3] -> Maximum: 5");
            Console.WriteLine("Window [5, 3, 6] -> Maximum: 6");
            Console.WriteLine("Window [3, 6, 7] -> Maximum: 7");
        }
    }
}
