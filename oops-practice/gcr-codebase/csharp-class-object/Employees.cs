using System;
class Employees
{
    string name;
    int id;
    int salary;
    public Employees(string empName, int empId, int empSalary)
    {
        name = empName;
        id = empId;
        salary = empSalary;
    }
    public void DisplayDetails()
    {
        Console.WriteLine("Employee Name: " + name);
        Console.WriteLine("Employee ID: " + id);
        Console.WriteLine("Employee Salary: " + salary);
    }
    public static void Main()
    {
        Employees emp1 = new Employees("Harsh", 101, 20000);
        emp1.DisplayDetails();
    }
}