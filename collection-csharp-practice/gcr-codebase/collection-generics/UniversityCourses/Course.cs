using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityCourseManagement
{
    /// <summary>
    /// Generic Course class with constraint T : CourseType
    /// Demonstrates: Generic Classes, Constraints
    /// </summary>
    public class Course<T> where T : CourseType
    {
        public string CourseId { get; set; }
        public T CourseDetails { get; set; }
        public List<string> EnrolledStudents { get; set; }
        public string Department { get; set; }
        public int Capacity { get; set; }

        public Course(string courseId, T courseDetails, string department, int capacity)
        {
            CourseId = courseId;
            CourseDetails = courseDetails;
            Department = department;
            Capacity = capacity;
            EnrolledStudents = new List<string>();
        }

        /// <summary>
        /// Enroll a student
        /// </summary>
        public bool EnrollStudent(string studentName)
        {
            if (EnrolledStudents.Count >= Capacity)
            {
                Console.WriteLine($"❌ Course {CourseDetails.CourseCode} is at full capacity!");
                return false;
            }

            if (EnrolledStudents.Contains(studentName))
            {
                Console.WriteLine($"❌ Student {studentName} already enrolled!");
                return false;
            }

            EnrolledStudents.Add(studentName);
            Console.WriteLine($"✓ Student {studentName} enrolled in {CourseDetails.CourseName}");
            return true;
        }

        /// <summary>
        /// Remove student from course
        /// </summary>
        public bool RemoveStudent(string studentName)
        {
            if (EnrolledStudents.Remove(studentName))
            {
                Console.WriteLine($"✓ Student {studentName} removed from {CourseDetails.CourseName}");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get available seats
        /// </summary>
        public int GetAvailableSeats()
        {
            return Capacity - EnrolledStudents.Count;
        }

        /// <summary>
        /// Display course details
        /// </summary>
        public void DisplayCourseInfo()
        {
            Console.WriteLine($"\n─── Course Information ───");
            Console.WriteLine($"Course ID: {CourseId}");
            Console.WriteLine($"Department: {Department}");
            CourseDetails.DisplayCourseDetails();
            Console.WriteLine($"Enrolled Students: {EnrolledStudents.Count}/{Capacity}");
            Console.WriteLine($"Available Seats: {GetAvailableSeats()}");
        }

        /// <summary>
        /// Display enrolled students
        /// </summary>
        public void DisplayEnrolledStudents()
        {
            Console.WriteLine($"\n─── Enrolled Students in {CourseDetails.CourseName} ───");
            if (EnrolledStudents.Count == 0)
            {
                Console.WriteLine("No students enrolled yet.");
                return;
            }

            for (int i = 0; i < EnrolledStudents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {EnrolledStudents[i]}");
            }
        }
    }
}
