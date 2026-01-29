using System;
using System.IO;
using System.Collections.Generic;

class SalaryUpdater
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("employees.csv");

        List<string> newData = new List<string>();

        // Add header
        newData.Add(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            if (parts[2] == "IT")
            {
                double sal = double.Parse(parts[3]);
                sal = sal + (sal * 10 / 100);
                parts[3] = sal.ToString();
            }

            newData.Add(parts[0] + "," + parts[1] + "," + parts[2] + "," + parts[3]);
        }

        File.WriteAllLines("updated_employees.csv", newData);

        Console.WriteLine("Salary Updated");
    }
}
