using System;
using System.Collections.Generic;

namespace csharp_modelling
{
    // University management: Students enroll in Courses; Professors teach Courses.
    class Course
    {
        public string Name { get; set; }
        public Professor AssignedProfessor { get; private set; }
        public List<Student> Students { get; private set; }

        public Course(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public void EnrollStudent(Student s)
        {
            if (!Students.Contains(s))
            {
                Students.Add(s);
                s.EnrollCourse(this);
            }
        }

        public void AssignProfessor(Professor prof)
        {
            AssignedProfessor = prof;
            prof.AssignCourse(this);
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

        public void EnrollCourse(Course c)
        {
            if (!Courses.Contains(c)) Courses.Add(c);
        }
    }

    class Professor
    {
        public string Name { get; set; }
        public List<Course> Courses { get; private set; }

        public Professor(string name)
        {
            Name = name;
            Courses = new List<Course>();
        }

        public void AssignCourse(Course c)
        {
            if (!Courses.Contains(c)) Courses.Add(c);
        }
    }

    class Program
    {
        static void Main()
        {
            var s1 = new Student("Nina");
            var prof = new Professor("Dr. Khan");
            var course = new Course("Data Structures");

            course.EnrollStudent(s1);
            course.AssignProfessor(prof);

            Console.WriteLine($"Course: {course.Name}");
            Console.WriteLine("Professor: " + course.AssignedProfessor.Name);
            Console.WriteLine("Students:");
            foreach (var s in course.Students) Console.WriteLine(" - " + s.Name);
        }
    }
}