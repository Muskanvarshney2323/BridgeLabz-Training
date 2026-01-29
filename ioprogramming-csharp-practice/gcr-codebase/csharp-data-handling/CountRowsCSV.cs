using System;
using System.IO;
using System.Linq;

class CsvRecordCounter
{
    static void Main(string[] args)
    {
        string path = "employees.csv";

        int recordCount = GetRecordCount(path);

        Console.WriteLine($"Total Records = {recordCount}");
    }

    static int GetRecordCount(string filePath)
    {
        return File.ReadLines(filePath)
                   .Skip(1)   // ignore header row
                   .Count();
    }
}
