using System;
using System.IO;

class CreateEmployeeCSV
{
    static void Main()
    {
        string fileName = "employees.csv";

        string[] rows = new string[]
        {
            "ID,Name,Department,Salary",
            "1,Amit,IT,50000",
            "2,Riya,HR,45000",
            "3,Rahul,Finance,60000",
            "4,Pooja,IT,55000",
            "5,Karan,Sales,48000"
        };

        File.WriteAllLines(fileName, rows);

        Console.WriteLine("Employee CSV Created Successfully");
    }
}
