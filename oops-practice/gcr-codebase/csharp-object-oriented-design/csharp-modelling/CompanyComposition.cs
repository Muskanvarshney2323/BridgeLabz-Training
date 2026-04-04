using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // Composition: Departments and Employees cannot exist without a Company.
    class Employee
    {
        public string Name { get; set; }
        public Employee(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class Department
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; private set; }

        public Department(string name)
        {
            Name = name;
            Employees = new List<Employee>();
        }

        public void AddEmployee(string empName)
        {
            Employees.Add(new Employee(empName));
        }

        public void Display()
        {
            Console.WriteLine($"Department: {Name}");
            foreach (var e in Employees) Console.WriteLine(" - " + e.Name);
        }
    }

    class Company : IDisposable
    {
        public string Name { get; set; }
        public List<Department> Departments { get; private set; }

        public Company(string name)
        {
            Name = name;
            Departments = new List<Department>();
        }

        public Department AddDepartment(string deptName)
        {
            var d = new Department(deptName);
            Departments.Add(d);
            return d;
        }

        // When company is disposed (deleted), departments and employees are cleared.
        public void Dispose()
        {
            Departments.Clear();
            Console.WriteLine($"{Name} disposed. All departments and employees removed.");
        }
    }

    class Program
    {
        static void Main()
        {
            var company = new Company("TechCorp");

            var dev = company.AddDepartment("Development");
            dev.AddEmployee("Alice");
            dev.AddEmployee("Bob");

            var hr = company.AddDepartment("HR");
            hr.AddEmployee("Carol");

            foreach (var d in company.Departments)
            {
                d.Display();
            }

            // Simulate deletion of company
            company.Dispose();
            Console.WriteLine("Departments after disposal: " + company.Departments.Count);
        }
    }
}