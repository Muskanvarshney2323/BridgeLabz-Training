using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class StudentFilterApp
{
    static void Main(string[] args)
    {
        string csvPath = "students.csv";

        IEnumerable<string> highScorers = GetHighScoreStudents(csvPath, 80);

        Display(highScorers);
    }

    static IEnumerable<string> GetHighScoreStudents(string filePath, int cutoff)
    {
        return File.ReadLines(filePath)
                   .Skip(1) // skip header row
                   .Where(row =>
                   {
                       string[] columns = row.Split(',');
                       int score = Convert.ToInt32(columns[3]);
                       return score > cutoff;
                   });
    }

    static void Display(IEnumerable<string> records)
    {
        foreach (string record in records)
        {
            Console.WriteLine(record);
        }
    }
}
