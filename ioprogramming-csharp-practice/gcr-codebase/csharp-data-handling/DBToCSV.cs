using System;
using System.Collections.Generic;
using System.IO;

class DatabaseExporter
{
    static void Main(string[] args)
    {
        string outputFile = "db_export.csv";

        List<string> records = FetchDatabaseRecords();

        SaveToCsv(outputFile, records);

        Console.WriteLine("Database data successfully saved as CSV");
    }

    static List<string> FetchDatabaseRecords()
    {
        return new List<string>
        {
            "1,Amit,IT,50000",
            "2,Riya,HR,45000"
        };
    }

    static void SaveToCsv(string filePath, List<string> content)
    {
        File.WriteAllLines(filePath, content);
    }
}
