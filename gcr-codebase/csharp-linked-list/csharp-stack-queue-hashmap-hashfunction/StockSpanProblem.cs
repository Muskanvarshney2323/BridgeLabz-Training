using System;
using System.Collections.Generic;

namespace StackQueueProblems
{
    /// <summary>
    /// Problem 3: Stock Span Problem
    /// 
    /// For each day in a stock price array, calculate the span 
    /// (number of consecutive days the price was less than or equal to the current day's price).
    /// 
    /// Hint: Use a stack to keep track of indices of prices in descending order.
    /// 
    /// Time Complexity: O(n) where n is the number of days
    /// Space Complexity: O(n) for the stack
    /// </summary>
    public class StockSpanProblem
    {
        /// <summary>
        /// Calculate the span for each day in the price array
        /// Returns an array where each element is the span for that day
        /// 
        /// Example: prices = [100, 80, 60, 70, 60, 75, 85]
        /// Output: [1, 1, 1, 2, 1, 4, 6]
        /// </summary>
        public static int[] CalculateSpan(int[] prices)
        {
            if (prices == null || prices.Length == 0)
                return new int[0];

            int n = prices.Length;
            int[] span = new int[n];
            Stack<int> stack = new Stack<int>(); // Stack to store indices

            for (int i = 0; i < n; i++)
            {
                // Pop elements from stack while current price is greater than or equal to stack top price
                // This removes indices of prices that don't affect the span
                while (stack.Count > 0 && prices[stack.Peek()] <= prices[i])
                {
                    stack.Pop();
                }

                // Calculate span:
                // If stack is empty, all previous elements have price <= current price
                // Otherwise, span is the distance from current index to the index on top of stack
                span[i] = (stack.Count == 0) ? (i + 1) : (i - stack.Peek());

                // Push current index to stack
                stack.Push(i);
            }

            return span;
        }

        /// <summary>
        /// Alternative approach using dynamic programming with stack
        /// </summary>
        public static int[] CalculateSpanOptimized(int[] prices)
        {
            if (prices == null || prices.Length == 0)
                return new int[0];

            int n = prices.Length;
            int[] span = new int[n];
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                // Pop while stack is not empty and top element price is less than or equal to current
                while (stack.Count > 0 && prices[stack.Peek()] <= prices[i])
                {
                    stack.Pop();
                }

                // Assign span value
                span[i] = (stack.Count == 0) ? (i + 1) : (i - stack.Peek());

                // Push current index
                stack.Push(i);
            }

            return span;
        }
    }

    // Example Usage
    public class StockSpanProblemExample
    {
        public static void Main()
        {
            int[] prices = { 100, 80, 60, 70, 60, 75, 85 };
            int[] span = StockSpanProblem.CalculateSpan(prices);

            Console.WriteLine("Stock Prices: " + string.Join(", ", prices));
            Console.WriteLine("Stock Span:   " + string.Join(", ", span));

            Console.WriteLine("\nExplanation:");
            for (int i = 0; i < prices.Length; i++)
            {
                Console.WriteLine($"Day {i + 1}: Price = {prices[i]}, Span = {span[i]}");
            }
        }
    }
}
