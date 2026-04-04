using System;

namespace csharp_inheritance
{
    class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double Salary { get; set; }

        public Employee(string name, int id, double salary)
        {
            Name = name;
            Id = id;
            Salary = salary;
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Name: {Name}, Id: {Id}, Salary: {Salary}");
        }
    }

    class Manager : Employee
    {
        public int TeamSize { get; set; }

        public Manager(string name, int id, double salary, int teamSize) : base(name, id, salary)
        {
            TeamSize = teamSize;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Role: Manager, Team Size: {TeamSize}");
        }
    }

    class Developer : Employee
    {
        public string ProgrammingLanguage { get; set; }

        public Developer(string name, int id, double salary, string lang) : base(name, id, salary)
        {
            ProgrammingLanguage = lang;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Role: Developer, Language: {ProgrammingLanguage}");
        }
    }

    class Intern : Employee
    {
        public string InternshipDuration { get; set; }

        public Intern(string name, int id, double salary, string duration) : base(name, id, salary)
        {
            InternshipDuration = duration;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Role: Intern, Duration: {InternshipDuration}");
        }
    }

    class Program
    {
        static void Main()
        {
            Employee[] emps = new Employee[]
            {
                new Manager("Alice", 1, 90000, 5),
                new Developer("Bob", 2, 80000, "C#"),
                new Intern("Charlie", 3, 15000, "3 months")
            };

            foreach (var e in emps)
            {
                e.DisplayDetails();
                Console.WriteLine();
            }
        }
    }
}