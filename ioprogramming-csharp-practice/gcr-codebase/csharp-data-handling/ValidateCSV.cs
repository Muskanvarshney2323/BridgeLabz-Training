using System;
using System.IO;
using System.Text.RegularExpressions;

class CSVValidator
{
    static void Main()
    {
        string fileName = "users.csv";

        // Create CSV file
        File.WriteAllLines(fileName, new string[]
        {
            "Name,Email,Phone",
            "Amit,amit@gmail.com,9876543210",
            "Riya,riya@,12345"
        });

        string[] rows = File.ReadAllLines(fileName);

        string emailRegex = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
        string phoneRegex = "^\\d{10}$";

        // Skip header
        for (int i = 1; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split(',');

            bool emailOk = Regex.IsMatch(columns[1], emailRegex);
            bool phoneOk = Regex.IsMatch(columns[2], phoneRegex);

            if (emailOk == false || phoneOk == false)
            {
                Console.WriteLine("Invalid Row: " + rows[i]);
            }
        }
    }
}
