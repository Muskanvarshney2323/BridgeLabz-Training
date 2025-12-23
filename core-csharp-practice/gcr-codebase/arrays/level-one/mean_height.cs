using System;
class MeanHeight
{
    static void Main()
    {
        double[] heights = new double[11];
        double sum = 0.0;

        Console.WriteLine("Enter the heights of 11 players in the football team:");

        for (int i = 0; i < heights.Length; i++)
        {
            Console.Write("Height of player {0}: ", i + 1);
            heights[i] = Convert.ToDouble(Console.ReadLine());
            sum += heights[i];
        }

        double meanHeight = sum / heights.Length;
        Console.WriteLine("The mean height of the football team is: " + meanHeight);
    }
}