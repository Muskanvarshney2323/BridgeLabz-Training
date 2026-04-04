using System;
using System.Reflection;

namespace CollectionReflection.BasicLevel
{
    /// <summary>
    /// Problem 4: Dynamically Create Objects
    /// Write a program to create an instance of a Student class dynamically 
    /// using Reflection without using the new keyword.
    /// </summary>
    class DynamicallyCreateObjects
    {
        static void Main()
        {
            Console.WriteLine("=== Dynamically Create Objects Using Reflection ===\n");

            // Method 1: Create instance using default constructor
            Type studentType = typeof(Student);
            Console.WriteLine("Method 1: Using Activator.CreateInstance (default constructor)");
            Student student1 = (Student)Activator.CreateInstance(studentType);
            Console.WriteLine($"Student created: {student1.Name}, Roll: {student1.RollNumber}\n");

            // Method 2: Create instance with parameters
            Console.WriteLine("Method 2: Using Activator.CreateInstance (with parameters)");
            Student student2 = (Student)Activator.CreateInstance(studentType, "Alice", 101);
            Console.WriteLine($"Student created: {student2.Name}, Roll: {student2.RollNumber}\n");

            // Method 3: Create instance using reflection (GetConstructor)
            Console.WriteLine("Method 3: Using ConstructorInfo.Invoke");
            ConstructorInfo[] constructors = studentType.GetConstructors();
            
            if (constructors.Length > 0)
            {
                // Get the constructor with parameters
                ConstructorInfo constructor = studentType.GetConstructor(new Type[] { typeof(string), typeof(int) });
                
                if (constructor != null)
                {
                    Student student3 = (Student)constructor.Invoke(new object[] { "Bob", 102 });
                    Console.WriteLine($"Student created: {student3.Name}, Roll: {student3.RollNumber}\n");
                }
            }

            // Method 4: Interactive object creation
            Console.WriteLine("--- Interactive Object Creation ---");
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();
            Console.Write("Enter roll number: ");
            int rollNumber = int.Parse(Console.ReadLine());

            Student studentDynamic = (Student)Activator.CreateInstance(studentType, name, rollNumber);
            Console.WriteLine($"\nStudent created dynamically: {studentDynamic.Name}, Roll: {studentDynamic.RollNumber}");
            studentDynamic.DisplayInfo();
        }
    }

    class Student
    {
        public string Name { get; set; }
        public int RollNumber { get; set; }

        // Default constructor
        public Student()
        {
            Name = "Unknown";
            RollNumber = 0;
        }

        // Parameterized constructor
        public Student(string name, int rollNumber)
        {
            Name = name;
            RollNumber = rollNumber;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Student Details: Name={Name}, Roll={RollNumber}");
        }
    }
}
