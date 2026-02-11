using System;
using System.Collections.Generic;

namespace StackQueueProblems
{
    public class StockSpanProblem
    {
        
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
