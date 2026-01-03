using System;

class Employee
{
    // Access modifiers
    public int employeeID;           // Public
    protected string department;     // Protected
    private double salary;           // Private

    // Constructor
    public Employee(int id, string dept, double sal)
    {
        employeeID = id;
        department = dept;
        salary = sal;
    }

    // Public method to modify salary
    public void SetSalary(double newSalary)
    {
        if (newSalary >= 0)
        {
            salary = newSalary;
        }
    }

    // Public method to access salary
    public double GetSalary()
    {
        return salary;
    }
}

// Subclass
class Manager : Employee
{
    string managerLevel;

    public Manager(int id, string dept, double sal, string level)
        : base(id, dept, sal)
    {
        managerLevel = level;
    }

    public void DisplayManagerDetails()
    {
        Console.WriteLine("Employee ID: " + employeeID); // public
        Console.WriteLine("Department: " + department); // protected
        Console.WriteLine("Salary: " + GetSalary());     // private via method
        Console.WriteLine("Manager Level: " + managerLevel);
    }
}

class Program
{
    static void Main()
    {
        Manager mgr =
            new Manager(501, "IT", 75000, "Senior Manager");

        mgr.DisplayManagerDetails();

        Console.WriteLine();

        // Modify salary using public method
        mgr.SetSalary(82000);
        Console.WriteLine("Updated Salary: " + mgr.GetSalary());
    }
}
