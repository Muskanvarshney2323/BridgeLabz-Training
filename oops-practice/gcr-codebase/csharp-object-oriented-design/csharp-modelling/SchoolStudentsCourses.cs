using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // Association (many-to-many) between Student and Course; School aggregates Students.
    class Course
    {
        public string Name { get; set; }
        public List<Student> EnrolledStudents { get; private set; }

        public Course(string name)
        {
            Name = name;
            EnrolledStudents = new List<Student>();
        }

        public void EnrollStudent(Student s)
        {
            if (!EnrolledStudents.Contains(s))
            {
                EnrolledStudents.Add(s);
                s.Courses.Add(this);
            }
        }

        public void DisplayStudents()
        {
            Console.WriteLine($"Course {Name} has students:");
            foreach (var s in EnrolledStudents) Console.WriteLine(" - " + s.Name);
        }
    }

    class Student
    {
        public string Name { get; set; }
        public List<Course> Courses { get; private set; }

        public Student(string name)
        {
            Name = name;
            Courses = new List<Course>();
        }

        public void ViewCourses()
        {
            Console.WriteLine($"{Name} enrolled in:");
            foreach (var c in Courses) Console.WriteLine(" - " + c.Name);
        }
    }

    class School
    {
        public string Name { get; set; }
        public List<Student> Students { get; private set; }

        public School(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public void AdmitStudent(Student s)
        {
            Students.Add(s); // aggregation: student could be created elsewhere
        }
    }

    class Program
    {
        static void Main()
        {
            var school = new School("Greenwood School");

            var s1 = new Student("Amy");
            var s2 = new Student("Ben");

            school.AdmitStudent(s1);
            school.AdmitStudent(s2);

            var math = new Course("Math");
            var cs = new Course("Computer Science");

            math.EnrollStudent(s1);
            cs.EnrollStudent(s1);
            cs.EnrollStudent(s2);

            s1.ViewCourses();
            cs.DisplayStudents();
        }
    }
}