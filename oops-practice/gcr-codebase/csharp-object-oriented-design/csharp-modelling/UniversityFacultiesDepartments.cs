using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // University composition with Departments (deleted with University)
    // Faculties are aggregated (can exist without a department)
    class Faculty
    {
        public string Name { get; set; }
        public Faculty(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }

    class Department
    {
        public string Name { get; set; }
        public List<Faculty> Faculties { get; private set; }

        public Department(string name)
        {
            Name = name;
            Faculties = new List<Faculty>();
        }

        public void AddFaculty(Faculty f)
        {
            Faculties.Add(f); // faculty aggregated
        }
    }

    class University : IDisposable
    {
        public string Name { get; set; }
        public List<Department> Departments { get; private set; }

        public University(string name)
        {
            Name = name;
            Departments = new List<Department>();
        }

        public Department AddDepartment(string name)
        {
            var d = new Department(name);
            Departments.Add(d);
            return d;
        }

        public void Dispose()
        {
            Departments.Clear();
            Console.WriteLine($"University {Name} disposed. All departments removed (composition).");
        }
    }

    class Program
    {
        static void Main()
        {
            var uni = new University("State University");

            var prof = new Faculty("Dr. Smith"); // faculty exists independently

            var csDept = uni.AddDepartment("Computer Science");
            csDept.AddFaculty(prof);

            Console.WriteLine($"Faculty {prof} assigned to {csDept.Name}");

            // Deleting university removes departments
            uni.Dispose();
            Console.WriteLine("Departments after dispose: " + uni.Departments.Count);

            Console.WriteLine($"Faculty still exists independently: {prof}");
        }
    }
}