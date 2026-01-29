using System;
using System.IO;
using System.Collections.Generic;

class SimpleCSVMerge
{
    static void Main()
    {
        // Create first CSV
        File.WriteAllLines("students1.csv",
            new string[] { "ID,Name,Age", "1,Amit,20" });

        // Create second CSV
        File.WriteAllLines("students2.csv",
            new string[] { "ID,Marks,Grade", "1,85,A" });

        // Read both files
        string[] file1 = File.ReadAllLines("students1.csv");
        string[] file2 = File.ReadAllLines("students2.csv");

        // Store marks and grade using ID
        Dictionary<string, string> data = new Dictionary<string, string>();

        for (int i = 1; i < file2.Length; i++)
        {
            string[] parts = file2[i].Split(',');
            data[parts[0]] = parts[1] + "," + parts[2];
        }

        // Prepare merged result
        List<string> output = new List<string>();
        output.Add("ID,Name,Age,Marks,Grade");

        for (int i = 1; i < file1.Length; i++)
        {
            string[] parts = file1[i].Split(',');
            output.Add(file1[i] + "," + data[parts[0]]);
        }

        // Write merged CSV
        File.WriteAllLines("merged.csv", output);

        Console.WriteLine("Merge Completed");
    }
}
