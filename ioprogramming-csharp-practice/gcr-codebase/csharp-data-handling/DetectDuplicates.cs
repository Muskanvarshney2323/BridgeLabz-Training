using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class CsvDuplicateFinder
{
    static void Main(string[] args)
    {
        string filePath = "students.csv";

        IEnumerable<string> duplicateRows = FindDuplicateEntries(filePath);

        PrintResults(duplicateRows);
    }

    static IEnumerable<string> FindDuplicateEntries(string path)
    {
        HashSet<string> seenKeys = new HashSet<string>();

        return File.ReadLines(path)
                   .Skip(1) // skip header
                   .Where(line =>
                   {
                       string key = line.Split(',')[0];
                       return !seenKeys.Add(key);
                   })
                   .Distinct();
    }

    static void PrintResults(IEnumerable<string> records)
    {
        foreach (string record in records)
        {
            Console.WriteLine(record);
        }
    }
}
