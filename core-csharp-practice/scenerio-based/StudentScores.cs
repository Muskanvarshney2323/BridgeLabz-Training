using System;

class StudentScores
{
    static void Main()
    {
        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());

        int[] scores = new int[n];
        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            Console.Write("Enter score: ");
            scores[i] = int.Parse(Console.ReadLine());

            if (scores[i] < 0)
            {
                Console.WriteLine("Invalid score!");
                return;
            }

            sum += scores[i];
        }

        double average = (double)sum / n;
        int max = scores[0], min = scores[0];

        for (int i = 0; i < n; i++)
        {
            if (scores[i] > max) max = scores[i];
            if (scores[i] < min) min = scores[i];
        }

        Console.WriteLine("Average: " + average);
        Console.WriteLine("Highest: " + max);
        Console.WriteLine("Lowest: " + min);

        Console.WriteLine("Scores above average:");
        for (int i = 0; i < n; i++)
        {
            if (scores[i] > average)
                Console.Write(scores[i] + " ");
        }
    }
}
