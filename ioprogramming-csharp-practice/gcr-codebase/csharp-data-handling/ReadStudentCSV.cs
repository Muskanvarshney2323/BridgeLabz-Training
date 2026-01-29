using System;
using System.IO;

class StudentCSVReader
{
    static void Main()
    {
        string fileName = "students.csv";

        // Create CSV file
        File.WriteAllLines(fileName, new string[]
        {
            "ID,Name,Age,Marks",
            "1,Amit,20,85",
            "2,Riya,19,78",
            "3,Rahul,21,90"
        });

        // Read CSV file
        string[] data = File.ReadAllLines(fileName);

        for (int i = 0; i < data.Length; i++)
        {
            string formattedLine = data[i].Replace(",", " | ");
            Console.WriteLine(formattedLine);
        }
    }
}
