using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface IDepartment
    {
        void AssignDepartment(string dept);
        string GetDepartmentDetails();
    }

    abstract class Employee
    {
        private int _employeeId;
        private string _name;
        private double _baseSalary;

        protected Employee(int id, string name, double baseSalary)
        {
            _employeeId = id;
            _name = name;
            _baseSalary = baseSalary;
        }

        public int EmployeeId => _employeeId;
        public string Name => _name;
        public double BaseSalary => _baseSalary;

        public abstract double CalculateSalary();

        public void DisplayDetails()
        {
            Console.WriteLine($"Id: {EmployeeId}, Name: {Name}, Salary: {CalculateSalary():C}");
        }
    }

    class FullTimeEmployee : Employee, IDepartment
    {
        private string _department;
        public FullTimeEmployee(int id, string name, double baseSalary) : base(id, name, baseSalary) { }
        public override double CalculateSalary() => BaseSalary; // simplified
        public void AssignDepartment(string dept) => _department = dept;
        public string GetDepartmentDetails() => _department ?? "Unassigned";
    }

    class PartTimeEmployee : Employee, IDepartment
    {
        private double _hourlyRate;
        private int _hours;
        private string _department;

        public PartTimeEmployee(int id, string name, double hourlyRate, int hours) : base(id, name, 0)
        {
            _hourlyRate = hourlyRate; _hours = hours;
        }

        public override double CalculateSalary() => _hourlyRate * _hours;
        public void AssignDepartment(string dept) => _department = dept;
        public string GetDepartmentDetails() => _department ?? "Unassigned";
    }

    class Program
    {
        static void Main()
        {
            var employees = new List<Employee>
            {
                new FullTimeEmployee(1, "Alice", 5000),
                new PartTimeEmployee(2, "Bob", 20, 80)
            };

            foreach (var e in employees)
            {
                e.DisplayDetails(); // polymorphism
            }
        }
    }
}