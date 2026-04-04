using System;

class MetalFactoryPipeCutting
{
    // Scenario A & B: Optimized revenue using Dynamic Programming
    static int GetOptimizedRevenue(int[] price, int rodLength)
    {
        int[] dp = new int[rodLength + 1];
        dp[0] = 0;

        for (int i = 1; i <= rodLength; i++)
        {
            int maxRevenue = int.MinValue;

            for (int j = 1; j <= i; j++)
            {
                maxRevenue = Math.Max(maxRevenue, price[j] + dp[i - j]);
            }

            dp[i] = maxRevenue;
        }

        return dp[rodLength];
    }

    // Scenario C: Non-optimized strategy (no cuts)
    static int GetNonOptimizedRevenue(int[] price, int rodLength)
    {
        return price[rodLength];
    }

    static void Main()
    {
        int rodLength = 8;

        // Original price chart
        int[] price = { 0, 1, 5, 8, 9, 10, 17, 17, 20 };

        Console.WriteLine("=== METAL FACTORY PIPE CUTTING SYSTEM ===\n");

        // Scenario A
        int optimizedRevenue = GetOptimizedRevenue(price, rodLength);
        Console.WriteLine("Scenario A: Optimized Revenue");
        Console.WriteLine("Maximum Revenue = ₹" + optimizedRevenue + "\n");

        // Scenario B: Custom length order added
        price[5] = 13; // custom order update
        int customOrderRevenue = GetOptimizedRevenue(price, rodLength);

        Console.WriteLine("Scenario B: Custom-Length Order Added");
        Console.WriteLine("Updated Price of Length 5 = ₹13");
        Console.WriteLine("New Maximum Revenue = ₹" + customOrderRevenue + "\n");

        // Scenario C: Non-optimized strategy
        int nonOptimizedRevenue = GetNonOptimizedRevenue(price, rodLength);
        Console.WriteLine("Scenario C: Non-Optimized Strategy");
        Console.WriteLine("Revenue Without Cutting = ₹" + nonOptimizedRevenue + "\n");

        // Final Comparison
        Console.WriteLine("=== REVENUE COMPARISON ===");
        Console.WriteLine("Optimized Strategy      : ₹" + optimizedRevenue);
        Console.WriteLine("With Custom Order       : ₹" + customOrderRevenue);
        Console.WriteLine("Non-Optimized Strategy  : ₹" + nonOptimizedRevenue);
    }
}
