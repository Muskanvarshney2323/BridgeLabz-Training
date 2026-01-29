using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

class DataFormatConverter
{
    static void Main(string[] args)
    {
        string jsonFile = "students.json";
        string csvFile = "students_from_json.csv";

        List<StudentInfo> data = CreateStudentData();

        SaveAsJson(jsonFile, data);

        SaveAsCsv(csvFile, data);

        Console.WriteLine("JSON and CSV conversion completed");
    }

    static List<StudentInfo> CreateStudentData()
    {
        return new List<StudentInfo>
        {
            new StudentInfo { RollNo = 1, StudentName = "Amit" },
            new StudentInfo { RollNo = 2, StudentName = "Riya" }
        };
    }

    static void SaveAsJson(string path, List<StudentInfo> records)
    {
        string jsonOutput = JsonSerializer.Serialize(records);
        File.WriteAllText(path, jsonOutput);
    }

    static void SaveAsCsv(string path, List<StudentInfo> records)
    {
        List<string> csvLines = new List<string> { "ID,Name" };

        foreach (var item in records)
        {
            csvLines.Add($"{item.RollNo},{item.StudentName}");
        }

        File.WriteAllLines(path, csvLines);
    }
}

class StudentInfo
{
    public int RollNo { get; set; }
    public string StudentName { get; set; }
}
