using System;
class EmployeeBonuses
{
    static void Main()
    {
        int employees = 10; Random rnd = new Random();
        double[,] data = new double[employees, 3]; // salary, years, bonus

        for (int i = 0; i < employees; i++)
        {
            int salary = rnd.Next(10000, 100000); // 5-digit-ish
            int yrs = rnd.Next(0, 11); // 0..10
            data[i,0] = salary; data[i,1] = yrs;
        }

        double totalOld = 0, totalNew = 0, totalBonus = 0;
        Console.WriteLine("Idx\tOldSalary\tYears\tBonus\tNewSalary");
        for (int i = 0; i < employees; i++)
        {
            double oldS = data[i,0]; int yrs = (int)data[i,1];
            double bonusRate = yrs > 5 ? 0.05 : 0.02;
            double bonus = oldS * bonusRate;
            double newS = oldS + bonus;
            data[i,2] = bonus;
            totalOld += oldS; totalNew += newS; totalBonus += bonus;
            Console.WriteLine($"{i+1}\t{oldS:F0}\t{yrs}\t{bonus:F2}\t{newS:F2}");
        }

        Console.WriteLine("\nTotals:");
        Console.WriteLine($"Sum Old Salary = {totalOld:F2}");
        Console.WriteLine($"Sum New Salary = {totalNew:F2}");
        Console.WriteLine($"Total Bonus = {totalBonus:F2}");
    }
}