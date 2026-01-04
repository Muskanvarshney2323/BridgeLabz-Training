using System;

namespace csharp_keywords
{
    class Student
    {
        public static string UniversityName = "State University";
        private static int totalStudents = 0;

        public readonly int RollNumber;
        public string Name;
        public string Grade;

        public Student(string Name, int RollNumber, string Grade)
        {
            this.Name = Name;
            this.RollNumber = RollNumber; // readonly
            this.Grade = Grade;
            totalStudents++;
        }

        public static void DisplayTotalStudents()
        {
            Console.WriteLine($"Total Students: {totalStudents}");
        }

        public void DisplayDetails(object obj)
        {
            if (obj is Student s)
            {
                Console.WriteLine("--- Student ---");
                Console.WriteLine($"University: {UniversityName}");
                Console.WriteLine($"Name: {s.Name}");
                Console.WriteLine($"Roll No: {s.RollNumber}");
                Console.WriteLine($"Grade: {s.Grade}");
            }
            else
            {
                Console.WriteLine("Object is not a Student instance.");
            }
        }

        static void Main()
        {
            var st = new Student("Sam", 201, "A");
            st.DisplayDetails(st);
            Student.DisplayTotalStudents();
        }
    }
}