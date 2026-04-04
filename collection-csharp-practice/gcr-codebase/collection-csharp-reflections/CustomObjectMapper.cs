using System;
using System.Collections.Generic;
using System.Reflection;

namespace CollectionReflection.AdvancedLevel
{
    /// <summary>
    /// Problem 8: Create a Custom Object Mapper
    /// Implement a method ToObject<T>(Type clazz, Dictionary<string, object> properties) 
    /// that uses Reflection to set field values from a given dictionary.
    /// </summary>
    class CustomObjectMapper
    {
        static void Main()
        {
            Console.WriteLine("=== Custom Object Mapper Using Reflection ===\n");

            // Example 1: Map dictionary to Student object
            var studentData = new Dictionary<string, object>
            {
                { "Name", "John Doe" },
                { "RollNumber", 101 },
                { "GPA", 3.8 }
            };

            Console.WriteLine("Example 1: Mapping to Student class");
            Student student = ObjectMapper.ToObject<Student>(studentData);
            Console.WriteLine($"Name: {student.Name}");
            Console.WriteLine($"Roll Number: {student.RollNumber}");
            Console.WriteLine($"GPA: {student.GPA}\n");

            // Example 2: Map dictionary to Employee object
            var employeeData = new Dictionary<string, object>
            {
                { "Name", "Alice Smith" },
                { "EmployeeId", 1001 },
                { "Department", "Engineering" },
                { "Salary", 75000 }
            };

            Console.WriteLine("Example 2: Mapping to Employee class");
            Employee employee = ObjectMapper.ToObject<Employee>(employeeData);
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Employee ID: {employee.EmployeeId}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Salary: {employee.Salary}\n");

            // Example 3: Partial mapping (missing fields)
            var partialData = new Dictionary<string, object>
            {
                { "Name", "Bob Wilson" },
                { "RollNumber", 102 }
            };

            Console.WriteLine("Example 3: Partial mapping (missing GPA)");
            Student student2 = ObjectMapper.ToObject<Student>(partialData);
            Console.WriteLine($"Name: {student2.Name}");
            Console.WriteLine($"Roll Number: {student2.RollNumber}");
            Console.WriteLine($"GPA: {student2.GPA}\n");

            // Example 4: Interactive mapping
            Console.WriteLine("--- Interactive Object Mapping ---");
            var userData = new Dictionary<string, object>();
            Console.Write("Enter Name: ");
            userData["Name"] = Console.ReadLine();
            Console.Write("Enter Roll Number: ");
            userData["RollNumber"] = int.Parse(Console.ReadLine());
            Console.Write("Enter GPA: ");
            userData["GPA"] = double.Parse(Console.ReadLine());

            Student userStudent = ObjectMapper.ToObject<Student>(userData);
            Console.WriteLine($"\nMapped Student: {userStudent.Name}, Roll: {userStudent.RollNumber}, GPA: {userStudent.GPA}");
        }
    }

    // Object Mapper class
    public static class ObjectMapper
    {
        /// <summary>
        /// Maps a dictionary of properties to an object of type T
        /// </summary>
        public static T ToObject<T>(Dictionary<string, object> properties) where T : class, new()
        {
            T obj = new T();
            Type objType = typeof(T);

            foreach (var kvp in properties)
            {
                try
                {
                    // Try to find a field with matching name
                    FieldInfo field = objType.GetField(kvp.Key, BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (field != null && field.CanWrite)
                    {
                        // Convert and set value
                        object convertedValue = Convert.ChangeType(kvp.Value, field.FieldType);
                        field.SetValue(obj, convertedValue);
                    }
                    else
                    {
                        // Try to find a property with matching name
                        PropertyInfo property = objType.GetProperty(kvp.Key, BindingFlags.Public | BindingFlags.IgnoreCase);
                        if (property != null && property.CanWrite)
                        {
                            object convertedValue = Convert.ChangeType(kvp.Value, property.PropertyType);
                            property.SetValue(obj, convertedValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not set property '{kvp.Key}': {ex.Message}");
                }
            }

            return obj;
        }
    }

    // Sample classes
    class Student
    {
        public string Name { get; set; }
        public int RollNumber { get; set; }
        public double GPA { get; set; }

        public override string ToString()
        {
            return $"Student[Name={Name}, Roll={RollNumber}, GPA={GPA}]";
        }
    }

    class Employee
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"Employee[Name={Name}, ID={EmployeeId}, Dept={Department}, Salary={Salary}]";
        }
    }
}
