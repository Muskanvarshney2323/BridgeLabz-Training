using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    
    public class StudentNode
    {
        public int RollNumber { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public StudentNode Next { get; set; }

        public StudentNode(int rollNumber, string name, int age, string grade)
        {
            RollNumber = rollNumber;
            Name = name;
            Age = age;
            Grade = grade;
            Next = null;
        }

        public override string ToString()
        {
            return $"[Roll: {RollNumber}, Name: {Name}, Age: {Age}, Grade: {Grade}]";
        }
    }

    public class StudentRecordManagement
    {
        private StudentNode head;

        public StudentRecordManagement()
        {
            head = null;
        }

        /// <summary>
        /// Add a new student record at the beginning
        /// </summary>
        public void AddAtBeginning(int rollNumber, string name, int age, string grade)
        {
            StudentNode newNode = new StudentNode(rollNumber, name, age, grade);
            newNode.Next = head;
            head = newNode;
            Console.WriteLine($"Student {name} (Roll: {rollNumber}) added at the beginning");
        }

        /// <summary>
        /// Add a new student record at the end
        /// </summary>
        public void AddAtEnd(int rollNumber, string name, int age, string grade)
        {
            StudentNode newNode = new StudentNode(rollNumber, name, age, grade);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                StudentNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            Console.WriteLine($"Student {name} (Roll: {rollNumber}) added at the end");
        }

        /// <summary>
        /// Add a new student record at a specific position
        /// </summary>
        public void AddAtPosition(int rollNumber, string name, int age, string grade, int position)
        {
            if (position == 0)
            {
                AddAtBeginning(rollNumber, name, age, grade);
                return;
            }

            StudentNode newNode = new StudentNode(rollNumber, name, age, grade);
            StudentNode current = head;
            int count = 0;

            while (current != null && count < position - 1)
            {
                current = current.Next;
                count++;
            }

            if (current == null)
            {
                Console.WriteLine("Position out of bounds");
                return;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
            Console.WriteLine($"Student {name} (Roll: {rollNumber}) added at position {position}");
        }

        /// <summary>
        /// Delete a student record by Roll Number
        /// </summary>
        public void DeleteByRollNumber(int rollNumber)
        {
            if (head == null)
            {
                Console.WriteLine("List is empty");
                return;
            }

            if (head.RollNumber == rollNumber)
            {
                head = head.Next;
                Console.WriteLine($"Student with Roll Number {rollNumber} deleted");
                return;
            }

            StudentNode current = head;
            while (current.Next != null && current.Next.RollNumber != rollNumber)
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                Console.WriteLine($"Student with Roll Number {rollNumber} deleted");
            }
            else
            {
                Console.WriteLine($"Student with Roll Number {rollNumber} not found");
            }
        }

        /// <summary>
        /// Search for a student record by Roll Number
        /// </summary>
        public StudentNode SearchByRollNumber(int rollNumber)
        {
            StudentNode current = head;
            while (current != null)
            {
                if (current.RollNumber == rollNumber)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Update a student's grade based on their Roll Number
        /// </summary>
        public void UpdateGrade(int rollNumber, string newGrade)
        {
            StudentNode student = SearchByRollNumber(rollNumber);
            if (student != null)
            {
                student.Grade = newGrade;
                Console.WriteLine($"Grade for student {student.Name} (Roll: {rollNumber}) updated to {newGrade}");
            }
            else
            {
                Console.WriteLine($"Student with Roll Number {rollNumber} not found");
            }
        }

        /// <summary>
        /// Display all student records
        /// </summary>
        public void DisplayAll()
        {
            if (head == null)
            {
                Console.WriteLine("No student records found");
                return;
            }

            Console.WriteLine("\n--- Student Records ---");
            StudentNode current = head;
            int count = 1;
            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Next;
                count++;
            }
            Console.WriteLine();
        }
    }

    // Example Usage
    public class StudentRecordManagementExample
    {
        public static void Main()
        {
            StudentRecordManagement system = new StudentRecordManagement();

            // Add students
            system.AddAtEnd(101, "Alice", 20, "A");
            system.AddAtEnd(102, "Bob", 21, "B");
            system.AddAtEnd(103, "Charlie", 19, "A");
            system.AddAtEnd(104, "Diana", 20, "C");

            system.AddAtBeginning(100, "Eve", 22, "B");
            system.AddAtPosition(105, "Frank", 20, "A", 2);

            system.DisplayAll();

            // Search for a student
            Console.WriteLine("Searching for student with Roll Number 103:");
            StudentNode found = system.SearchByRollNumber(103);
            Console.WriteLine(found != null ? found.ToString() : "Not found");

            // Update grade
            system.UpdateGrade(102, "A");

            // Delete a student
            system.DeleteByRollNumber(104);

            system.DisplayAll();
        }
    }
}
