using System;
using System.IO;

class CSVReader
{
    static void Main()
    {
        StreamReader sr = new StreamReader("employees.csv");

        int rowCount = 0;
        string line;

        while (true)
        {
            line = sr.ReadLine();

            if (line == null)
                break;

            rowCount++;

            if (rowCount % 100 == 0)
            {
                Console.WriteLine("Processed: " + rowCount);
            }
        }

        sr.Close();
    }
}
