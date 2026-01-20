using System;

namespace UniversityCourseManagement
{
    /// <summary>
    /// Problem 3: Multi-Level University Course Management System
    /// Demonstrates: Generic Classes, Constraints, Variance
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║  PROBLEM 3: University Course Management      ║");
            Console.WriteLine("║ Concepts: Generics, Constraints, Variance    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝\n");

            Department computerScience = new Department("Computer Science");

            // Create exam courses
            Console.WriteLine("─── Creating Exam-Based Courses ───");
            var examCourse1 = new Course<ExamCourse>(
                "CS101",
                new ExamCourse("Data Structures", "DS101", 4, 30, 70),
                "Computer Science",
                50
            );

            var examCourse2 = new Course<ExamCourse>(
                "CS102",
                new ExamCourse("Algorithms", "ALG101", 3, 25, 75),
                "Computer Science",
                45
            );

            computerScience.AddExamCourse(examCourse1);
            computerScience.AddExamCourse(examCourse2);

            // Create assignment courses
            Console.WriteLine("\n─── Creating Assignment-Based Courses ───");
            var assignmentCourse1 = new Course<AssignmentCourse>(
                "CS201",
                new AssignmentCourse("Web Development", "WEB201", 3, 40, 40, 20),
                "Computer Science",
                60
            );

            computerScience.AddAssignmentCourse(assignmentCourse1);

            // Create practical courses
            Console.WriteLine("\n─── Creating Practical Courses ───");
            var practicalCourse1 = new Course<PracticalCourse>(
                "CS301",
                new PracticalCourse("Database Lab", "DB301", 2, 50, 30, 20),
                "Computer Science",
                40
            );

            computerScience.AddPracticalCourse(practicalCourse1);

            // Enroll students
            Console.WriteLine("\n─── Enrolling Students ───");
            examCourse1.EnrollStudent("Alice Johnson");
            examCourse1.EnrollStudent("Bob Smith");
            examCourse1.EnrollStudent("Charlie Brown");

            examCourse2.EnrollStudent("David Lee");
            examCourse2.EnrollStudent("Eve Davis");

            assignmentCourse1.EnrollStudent("Frank Miller");
            assignmentCourse1.EnrollStudent("Grace Wilson");
            assignmentCourse1.EnrollStudent("Henry Taylor");

            practicalCourse1.EnrollStudent("Iris Anderson");
            practicalCourse1.EnrollStudent("Jack White");

            // Display all courses
            computerScience.DisplayAllCourses();

            // Display enrolled students for a specific course
            Console.WriteLine("\n─── Students in Data Structures ───");
            examCourse1.DisplayEnrolledStudents();

            // Try to enroll in full course
            Console.WriteLine("\n─── Testing Enrollment Limits ───");
            var fullCourse = new Course<ExamCourse>(
                "CS401",
                new ExamCourse("Advanced Topics", "ADV401", 4, 30, 70),
                "Computer Science",
                2
            );

            fullCourse.EnrollStudent("Student 1");
            fullCourse.EnrollStudent("Student 2");
            fullCourse.EnrollStudent("Student 3"); // Should fail

            // Display department statistics
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("DEPARTMENT STATISTICS:");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine($"Total Enrollments: {computerScience.GetTotalEnrollments()}");
            Console.WriteLine($"Total Capacity: {computerScience.GetTotalCapacity()}");
            Console.WriteLine($"Utilization: {(computerScience.GetTotalEnrollments() * 100.0 / computerScience.GetTotalCapacity()):F1}%");
            Console.WriteLine("═══════════════════════════════════════════════\n");
        }
    }
}
