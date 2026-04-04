using System;
class EmployeesSalary
{
    static void Main()
    {
        const int employeeCount = 10;
        double[] salaries = new double[employeeCount];
        double[] yearsOfService = new double[employeeCount];
        double[] bonuses = new double[employeeCount];
        double[] newSalaries = new double[employeeCount];

        double totalBonus = 0.0;
        double totalOldSalary = 0.0;
        double totalNewSalary = 0.0;

        for (int i = 0; i < employeeCount; i++)
        {
            Console.WriteLine($"Enter salary for employee {i + 1}: ");
            double salary = Convert.ToDouble(Console.ReadLine());
            if (salary <= 0)
            {
                Console.WriteLine("Invalid salary. Please enter again.");
                i--;
                continue;
            }
            salaries[i] = salary;

            Console.WriteLine($"Enter years of service for employee {i + 1}: ");
            double years = Convert.ToDouble(Console.ReadLine());
            if (years < 0)
            {
                Console.WriteLine("Invalid years of service. Please enter again.");
                i--;
                continue;
            }
            yearsOfService[i] = years;
        }

        for (int i = 0; i < employeeCount; i++)
        {
            double bonusPercentage = yearsOfService[i] > 5 ? 0.05 : 0.02;
            bonuses[i] = salaries[i] * bonusPercentage;
            newSalaries[i] = salaries[i] + bonuses[i];

            totalBonus += bonuses[i];
            totalOldSalary += salaries[i];
            totalNewSalary += newSalaries[i];
        }

        Console.WriteLine($"Total bonus payout: {totalBonus}");
        Console.WriteLine($"Total old salary: {totalOldSalary}");
        Console.WriteLine($"Total new salary: {totalNewSalary}");
    }
}