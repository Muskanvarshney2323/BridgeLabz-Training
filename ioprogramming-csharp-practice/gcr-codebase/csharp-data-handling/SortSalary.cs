using System;
using System.IO;

class SortEmployeeSalary
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("employees.csv");

        // Skip header (index 0)
        for (int i = 1; i < lines.Length - 1; i++)
        {
            for (int j = i + 1; j < lines.Length; j++)
            {
                int salary1 = int.Parse(lines[i].Split(',')[3]);
                int salary2 = int.Parse(lines[j].Split(',')[3]);

                // Swap if salary is smaller
                if (salary1 < salary2)
                {
                    string temp = lines[i];
                    lines[i] = lines[j];
                    lines[j] = temp;
                }
            }
        }

        // Print top 5 employees
        for (int i = 1; i <= 5 && i < lines.Length; i++)
        {
            Console.WriteLine(lines[i]);
        }
    }
}
