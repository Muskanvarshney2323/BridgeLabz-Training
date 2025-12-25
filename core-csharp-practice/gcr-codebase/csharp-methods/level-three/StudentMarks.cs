using System;
class StudentMarks
{
    static void Main()
    {
        Console.Write("Enter number of students: "); int n = Convert.ToInt32(Console.ReadLine());
        int[,] scores = GenerateRandomScores(n);
        double[,] results = CalculateTotalsAveragesPercentages(scores);
        DisplayScorecard(scores, results);
    }

    static int[,] GenerateRandomScores(int n)
    {
        var rnd = new Random();
        int[,] s = new int[n,3];
        for (int i = 0; i < n; i++) { s[i,0] = rnd.Next(10,100); s[i,1] = rnd.Next(10,100); s[i,2] = rnd.Next(10,100); }
        return s;
    }

    static double[,] CalculateTotalsAveragesPercentages(int[,] s)
    {
        int n = s.GetLength(0);
        double[,] outArr = new double[n,3]; // total, avg, percent
        for (int i = 0; i < n; i++)
        {
            double total = s[i,0] + s[i,1] + s[i,2];
            double avg = Math.Round(total / 3.0, 2);
            double percent = Math.Round((total / 300.0) * 100.0, 2);
            outArr[i,0] = total; outArr[i,1] = avg; outArr[i,2] = percent;
        }
        return outArr;
    }

    static void DisplayScorecard(int[,] s, double[,] r)
    {
        Console.WriteLine("Idx\tP\tC\tM\tTotal\tAvg\tPercent");
        for (int i = 0; i < s.GetLength(0); i++)
        {
            Console.WriteLine($"{i+1}\t{s[i,0]}\t{s[i,1]}\t{s[i,2]}\t{r[i,0]:F0}\t{r[i,1]:F2}\t{r[i,2]:F2}");
        }
    }
}