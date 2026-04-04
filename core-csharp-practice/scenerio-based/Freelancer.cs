using System;

class InvoiceGenerator
{
    static string[] ParseInvoice(string input)
    {
        return input.Split(',');
    }

    static double GetTotalAmount(string[] tasks)
    {
        double total = 0;
        foreach (string task in tasks)
        {
            string[] parts = task.Split('-');
            if (parts.Length >= 2)
            {
                double amount = double.Parse(parts[1].Trim().Split(' ')[0]);
                total += amount;
            }
        }
        return total;
    }

    static void Main()
    {
        Console.Write("Enter invoice details: ");
        string input = Console.ReadLine();

        string[] tasks = ParseInvoice(input);
        double total = GetTotalAmount(tasks);

        Console.WriteLine("Total Invoice Amount: " + total);
    }
}