using System;
class FootballTeamHeights
{
    static void Main()
    {
        int[] heights = GenerateRandomHeights(11, 150, 250);
        Console.WriteLine("Heights (cm): " + string.Join(", ", heights));

        int sum = Sum(heights);
        double mean = Mean(heights);
        int min = Min(heights);
        int max = Max(heights);

        Console.WriteLine($"Sum = {sum}");
        Console.WriteLine($"Mean = {mean:F2} cm");
        Console.WriteLine($"Shortest = {min} cm");
        Console.WriteLine($"Tallest = {max} cm");
    }

    static int[] GenerateRandomHeights(int count, int min, int max)
    {
        Random rnd = new Random();
        int[] arr = new int[count];
        for (int i = 0; i < count; i++) arr[i] = rnd.Next(min, max + 1);
        return arr;
    }

    static int Sum(int[] arr)
    {
        int s = 0;
        foreach (var v in arr) s += v;
        return s;
    }

    static double Mean(int[] arr) => (double)Sum(arr) / arr.Length;
    static int Min(int[] arr) { int m = arr[0]; foreach (var v in arr) if (v < m) m = v; return m; }
    static int Max(int[] arr) { int m = arr[0]; foreach (var v in arr) if (v > m) m = v; return m; }
}