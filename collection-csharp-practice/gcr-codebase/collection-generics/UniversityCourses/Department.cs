using System;
using System.Collections.Generic;

namespace UniversityCourseManagement
{
    /// <summary>
    /// Department class managing courses of different types
    /// </summary>
    public class Department
    {
        public string DepartmentName { get; set; }
        private List<Course<ExamCourse>> examCourses;
        private List<Course<AssignmentCourse>> assignmentCourses;
        private List<Course<PracticalCourse>> practicalCourses;

        public Department(string name)
        {
            DepartmentName = name;
            examCourses = new List<Course<ExamCourse>>();
            assignmentCourses = new List<Course<AssignmentCourse>>();
            practicalCourses = new List<Course<PracticalCourse>>();
        }

        /// <summary>
        /// Add exam course
        /// </summary>
        public void AddExamCourse(Course<ExamCourse> course)
        {
            examCourses.Add(course);
            Console.WriteLine($"✓ Added exam course: {course.CourseDetails.CourseName}");
        }

        /// <summary>
        /// Add assignment course
        /// </summary>
        public void AddAssignmentCourse(Course<AssignmentCourse> course)
        {
            assignmentCourses.Add(course);
            Console.WriteLine($"✓ Added assignment course: {course.CourseDetails.CourseName}");
        }

        /// <summary>
        /// Add practical course
        /// </summary>
        public void AddPracticalCourse(Course<PracticalCourse> course)
        {
            practicalCourses.Add(course);
            Console.WriteLine($"✓ Added practical course: {course.CourseDetails.CourseName}");
        }

        /// <summary>
        /// Display all courses
        /// </summary>
        public void DisplayAllCourses()
        {
            Console.WriteLine($"\n╔═══════════════════════════════════════════════╗");
            Console.WriteLine($"║  {DepartmentName} - All Courses");
            Console.WriteLine($"╚═══════════════════════════════════════════════╝");

            Console.WriteLine("\n─── Exam-Based Courses ───");
            foreach (var course in examCourses)
            {
                course.DisplayCourseInfo();
            }

            Console.WriteLine("\n─── Assignment-Based Courses ───");
            foreach (var course in assignmentCourses)
            {
                course.DisplayCourseInfo();
            }

            Console.WriteLine("\n─── Practical Courses ───");
            foreach (var course in practicalCourses)
            {
                course.DisplayCourseInfo();
            }
        }

        /// <summary>
        /// Get total enrollments across all courses
        /// </summary>
        public int GetTotalEnrollments()
        {
            int total = 0;
            foreach (var course in examCourses)
                total += course.EnrolledStudents.Count;
            foreach (var course in assignmentCourses)
                total += course.EnrolledStudents.Count;
            foreach (var course in practicalCourses)
                total += course.EnrolledStudents.Count;

            return total;
        }

        /// <summary>
        /// Get total course capacity
        /// </summary>
        public int GetTotalCapacity()
        {
            int total = 0;
            foreach (var course in examCourses)
                total += course.Capacity;
            foreach (var course in assignmentCourses)
                total += course.Capacity;
            foreach (var course in practicalCourses)
                total += course.Capacity;

            return total;
        }
    }
}
