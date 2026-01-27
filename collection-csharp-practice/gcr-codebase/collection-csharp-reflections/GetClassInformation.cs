using System;
using System.Reflection;

namespace CollectionReflection.BasicLevel
{
    /// <summary>
    /// Problem 1: Get Class Information
    /// Write a program to accept a class name as input and display its methods, 
    /// fields, and constructors using Reflection.
    /// </summary>
    class GetClassInformation
    {
        static void Main()
        {
            Console.WriteLine("=== Get Class Information Using Reflection ===\n");

            // Sample class to inspect
            Type studentType = typeof(Student);

            // Display class name
            Console.WriteLine($"Class Name: {studentType.Name}\n");

            // Get and display all methods
            Console.WriteLine("--- Methods ---");
            MethodInfo[] methods = studentType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"  {method.ReturnType.Name} {method.Name}()");
            }

            // Get and display all fields
            Console.WriteLine("\n--- Fields ---");
            FieldInfo[] fields = studentType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"  {field.FieldType.Name} {field.Name}");
            }

            // Get and display all constructors
            Console.WriteLine("\n--- Constructors ---");
            ConstructorInfo[] constructors = studentType.GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                string paramList = string.Join(", ", Array.ConvertAll(parameters, p => p.ParameterType.Name + " " + p.Name));
                Console.WriteLine($"  Student({paramList})");
            }

            // Interactive mode: Accept class name as input
            Console.WriteLine("\n\n--- Enter a Class Name to Inspect ---");
            Console.Write("Enter class name (e.g., System.String, System.Int32): ");
            string className = Console.ReadLine();

            try
            {
                Type type = Type.GetType(className);
                if (type == null)
                {
                    Console.WriteLine("Class not found!");
                    return;
                }

                Console.WriteLine($"\nClass: {type.Name}");
                Console.WriteLine("Methods:");
                MethodInfo[] typeMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in typeMethods)
                {
                    Console.WriteLine($"  {method.ReturnType.Name} {method.Name}()");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Sample class for reflection
    class Student
    {
        public string Name;
        public int RollNumber;

        public Student() { }

        public Student(string name, int rollNumber)
        {
            Name = name;
            RollNumber = rollNumber;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Student: {Name}, Roll: {RollNumber}");
        }

        public string GetGrade()
        {
            return "A";
        }
    }
}
