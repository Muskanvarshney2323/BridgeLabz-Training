using System;

class TemperatureAnalyzer
{
    static void Main()
    {
        // Generate sample week's data (7 days × 24 hours) and analyze
        float[,] temps = GenerateSampleTemperatures();

        int hottestDay = FindHottestDay(temps);
        int coldestDay = FindColdestDay(temps);
        float[] dailyAverages = AverageTemperaturePerDay(temps);

        Console.WriteLine($"Hottest day: Day {hottestDay + 1} (max {DayMax(temps, hottestDay):F2}°)");
        Console.WriteLine($"Coldest day: Day {coldestDay + 1} (min {DayMin(temps, coldestDay):F2}°)");
        Console.WriteLine("Average temperature per day (°):");
        for (int d = 0; d < dailyAverages.Length; d++)
        {
            Console.WriteLine($" Day {d + 1}: {dailyAverages[d]:F2}");
        }
    }

    // Sample data generator (temperatures in °C)
    public static float[,] GenerateSampleTemperatures()
    {
        var rnd = new Random(0);
        float[,] a = new float[7, 24];
        for (int d = 0; d < 7; d++)
            for (int h = 0; h < 24; h++)
                a[d, h] = (float)(rnd.NextDouble() * 25 + 5); // 5.0 to 30.0 °C
        return a;
    }

    // Returns the day index (0..6) which contains the single highest temperature in the week
    public static int FindHottestDay(float[,] temps)
    {
        int days = temps.GetLength(0);
        int hours = temps.GetLength(1);
        float best = float.MinValue;
        int bestDay = 0;
        for (int d = 0; d < days; d++)
            for (int h = 0; h < hours; h++)
                if (temps[d, h] > best)
                {
                    best = temps[d, h];
                    bestDay = d;
                }
        return bestDay;
    }

    // Returns the day index (0..6) which contains the single lowest temperature in the week
    public static int FindColdestDay(float[,] temps)
    {
        int days = temps.GetLength(0);
        int hours = temps.GetLength(1);
        float best = float.MaxValue;
        int bestDay = 0;
        for (int d = 0; d < days; d++)
            for (int h = 0; h < hours; h++)
                if (temps[d, h] < best)
                {
                    best = temps[d, h];
                    bestDay = d;
                }
        return bestDay;
    }

    // Returns average temperature for each day (array length 7)
    public static float[] AverageTemperaturePerDay(float[,] temps)
    {
        int days = temps.GetLength(0);
        int hours = temps.GetLength(1);
        float[] avgs = new float[days];
        for (int d = 0; d < days; d++)
        {
            float sum = 0f;
            for (int h = 0; h < hours; h++) sum += temps[d, h];
            avgs[d] = sum / hours;
        }
        return avgs;
    }

    // Helpers
    public static float DayMax(float[,] temps, int day)
    {
        int hours = temps.GetLength(1);
        float m = float.MinValue;
        for (int h = 0; h < hours; h++) if (temps[day, h] > m) m = temps[day, h];
        return m;
    }

    public static float DayMin(float[,] temps, int day)
    {
        int hours = temps.GetLength(1);
        float m = float.MaxValue;
        for (int h = 0; h < hours; h++) if (temps[day, h] < m) m = temps[day, h];
        return m;
    }
}