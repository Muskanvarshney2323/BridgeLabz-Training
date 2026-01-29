using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Learner
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public int StudentAge { get; set; }
}

class CsvReaderApp
{
    static void Main(string[] args)
    {
        string filePath = "students.csv";

        List<Learner> students = LoadStudentsFromCsv(filePath);

        DisplayStudents(students);
    }

    static List<Learner> LoadStudentsFromCsv(string path)
    {
        return File.ReadLines(path)
                   .Skip(1) // skip header
                   .Select(ConvertLineToStudent)
                   .ToList();
    }

    static Learner ConvertLineToStudent(string row)
    {
        string[] values = row.Split(',');

        return new Learner
        {
            StudentId = Convert.ToInt32(values[0]),
            StudentName = values[1],
            StudentAge = Convert.ToInt32(values[2])
        };
    }

    static void DisplayStudents(IEnumerable<Learner> data)
    {
        foreach (Learner item in data)
        {
            Console.WriteLine(
                $"ID: {item.StudentId}, Name: {item.StudentName}, Age: {item.StudentAge}"
            );
        }
    }
}
