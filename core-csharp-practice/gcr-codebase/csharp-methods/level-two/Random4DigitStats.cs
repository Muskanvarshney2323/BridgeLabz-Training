using System;
class Random4DigitStats
{
    static void Main()
    {
        int[] arr = Generate4DigitRandomArray(5);
        Console.WriteLine("Numbers: " + string.Join(", ", arr));
        double[] stats = FindAverageMinMax(arr);
        Console.WriteLine($"Average = {stats[0]:F2}, Min = {stats[1]}, Max = {stats[2]}");
    }

    public static int[] Generate4DigitRandomArray(int size)
    {
        Random rnd = new Random();
        int[] a = new int[size];
        for (int i = 0; i < size; i++) a[i] = rnd.Next(1000, 10000);
        return a;
    }

    public static double[] FindAverageMinMax(int[] numbers)
    {
        if (numbers.Length == 0) return new double[] { 0, 0, 0 };
        double sum = 0; int min = numbers[0], max = numbers[0];
        foreach (int v in numbers) { sum += v; if (v < min) min = v; if (v > max) max = v; }
        return new double[] { sum / numbers.Length, min, max };
    }
}