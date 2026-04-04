using System;
using System.Collections.Generic;

public class EmployeeUtilityImpl : IEmployee
{
    private static List<Employee> employees = new List<Employee>();

    public void AddEmployee()
    {
        Console.Write("Enter Employee ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Employee Name: ");
        string name = Console.ReadLine();

        Console.Write("Is Employee Present? (yes/no): ");
        bool present = Console.ReadLine().ToLower() == "yes";

        Employee emp = new Employee(id, name, present);
        employees.Add(emp);

        Console.WriteLine(" Employee Added Successfully");
    }

    public void CheckEmployeeAttendance()
    {
        Console.Write("Enter Employee ID: ");
        int id = int.Parse(Console.ReadLine());

        foreach (Employee emp in employees)
        {
            if (emp.GetEmpID() == id)
            {
                if (emp.IsPresent())
                    Console.WriteLine($" {emp.GetEmpName()} is PRESENT");
                else
                    Console.WriteLine($" {emp.GetEmpName()} is ABSENT");
                return;
            }
        }

        Console.WriteLine(" Employee Not Found");
    }
    public void CalculateDailyWage()
    {
        Console.Write("Enter Employee ID: ");
        int id = int.Parse(Console.ReadLine());

        foreach (Employee emp in employees)
        {
            if (emp.GetEmpID() == id)
            {
                Console.WriteLine($" Daily Wage of {emp.GetEmpName()} = ₹{emp.GetDailyWage()}");
                return;
            }
        }

        Console.WriteLine(" Employee Not Found");
    }
}
