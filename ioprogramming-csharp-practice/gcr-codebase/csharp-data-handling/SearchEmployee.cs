using System;
using System.IO;

class EmployeeSearch
{
    static void Main()
    {
        Console.Write("Enter Name: ");
        string searchName = Console.ReadLine();

        string[] records = File.ReadAllLines("employees.csv");

        for (int i = 0; i < records.Length; i++)
        {
            string line = records[i];

            if (line.Contains(searchName))
            {
                string[] parts = line.Split(',');

                Console.WriteLine("Department: " + parts[2]);
                Console.WriteLine("Salary: " + parts[3]);
            }
        }
    }
}
