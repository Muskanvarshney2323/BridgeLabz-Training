using System;

namespace csharp_keywords
{
    class Employee
    {
        public static string CompanyName = "Tech Solutions";
        private static int totalEmployees = 0;

        public readonly int Id;
        public string Name;
        public string Designation;

        public Employee(string Name, int Id, string Designation)
        {
            this.Name = Name;
            this.Id = Id; // readonly assigned
            this.Designation = Designation;
            totalEmployees++;
        }

        public static void DisplayTotalEmployees()
        {
            Console.WriteLine($"Total Employees: {totalEmployees}");
        }

        public void DisplayDetails(object obj)
        {
            if (obj is Employee e)
            {
                Console.WriteLine("--- Employee ---");
                Console.WriteLine($"Company: {CompanyName}");
                Console.WriteLine($"Name: {e.Name}");
                Console.WriteLine($"Id: {e.Id}");
                Console.WriteLine($"Designation: {e.Designation}");
            }
            else
            {
                Console.WriteLine("Object is not an Employee instance.");
            }
        }

        static void Main()
        {
            var emp1 = new Employee("John", 101, "Developer");
            var emp2 = new Employee("Jane", 102, "Designer");

            emp1.DisplayDetails(emp1);
            Employee.DisplayTotalEmployees();
        }
    }
}