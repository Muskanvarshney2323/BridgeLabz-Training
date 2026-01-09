using System;
using System.Collections.Generic;

class ATMDispenser
{
    static void DispenseCash(int amount, int[] notes)
    {
        Dictionary<int, int> result = new Dictionary<int, int>();

        foreach (int note in notes)
        {
            if (amount >= note)
            {
                int count = amount / note;
                amount %= note;
                result[note] = count;
            }
        }

        Console.WriteLine("Dispensed Notes:");
        foreach (var item in result)
        {
            Console.WriteLine($"₹{item.Key} x {item.Value}");
        }

        if (amount > 0)
        {
            Console.WriteLine($"⚠ Remaining amount ₹{amount} cannot be dispensed.");
        }
    }

    static void Main()
    {
        Console.WriteLine("Scenario A: With ₹500 note");
        int[] notesA = { 500, 200, 100, 50, 20, 10, 5, 2, 1 };
        DispenseCash(880, notesA);

        Console.WriteLine("\nScenario B: Without ₹500 note");
        int[] notesB = { 200, 100, 50, 20, 10, 5, 2, 1 };
        DispenseCash(880, notesB);

        Console.WriteLine("\nScenario C: Fallback case");
        int[] notesC = { 5, 2 };
        DispenseCash(3, notesC);
    }
}
