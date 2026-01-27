using System;
using System.Reflection;
using System.Text;

namespace CollectionReflection.AdvancedLevel
{
    /// <summary>
    /// Problem 9: Generate a JSON Representation
    /// Write a program that converts an object to a JSON-like string using Reflection 
    /// by inspecting its fields and values.
    /// </summary>
    class GenerateJsonRepresentation
    {
        static void Main()
        {
            Console.WriteLine("=== Generate JSON Representation Using Reflection ===\n");

            // Example 1: Simple object
            Student student = new Student { Name = "John Doe", RollNumber = 101, GPA = 3.8 };
            Console.WriteLine("Example 1: Student Object");
            string jsonStudent = ObjectToJson(student);
            Console.WriteLine(jsonStudent);

            // Example 2: Complex object with nested data
            Console.WriteLine("\n\nExample 2: Person Object");
            Person person = new Person
            {
                FirstName = "Alice",
                LastName = "Smith",
                Age = 25,
                IsActive = true
            };
            string jsonPerson = ObjectToJson(person);
            Console.WriteLine(jsonPerson);

            // Example 3: Employee object
            Console.WriteLine("\n\nExample 3: Employee Object");
            Employee employee = new Employee
            {
                Name = "Bob Johnson",
                EmployeeId = 1001,
                Department = "Engineering",
                Salary = 85000
            };
            string jsonEmployee = ObjectToJson(employee);
            Console.WriteLine(jsonEmployee);

            // Example 4: Array of objects
            Console.WriteLine("\n\nExample 4: Array of Students");
            Student[] students = new Student[]
            {
                new Student { Name = "John", RollNumber = 101, GPA = 3.8 },
                new Student { Name = "Jane", RollNumber = 102, GPA = 3.9 },
                new Student { Name = "Jack", RollNumber = 103, GPA = 3.7 }
            };
            
            Console.WriteLine("[");
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine(ObjectToJson(students[i], "  "));
                if (i < students.Length - 1)
                    Console.WriteLine(",");
            }
            Console.WriteLine("]");
        }

        /// <summary>
        /// Converts an object to JSON representation using Reflection
        /// </summary>
        static string ObjectToJson(object obj, string indent = "")
        {
            if (obj == null)
                return "null";

            Type objType = obj.GetType();
            StringBuilder json = new StringBuilder();

            json.Append(indent + "{\n");

            FieldInfo[] fields = objType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int totalMembers = fields.Length + properties.Length;
            int currentMember = 0;

            // Handle fields
            foreach (FieldInfo field in fields)
            {
                currentMember++;
                object value = field.GetValue(obj);
                string jsonValue = GetJsonValue(value);
                json.Append(indent + $"  \"{field.Name}\": {jsonValue}");
                
                if (currentMember < totalMembers)
                    json.Append(",");
                
                json.Append("\n");
            }

            // Handle properties
            foreach (PropertyInfo property in properties)
            {
                currentMember++;
                try
                {
                    object value = property.GetValue(obj);
                    string jsonValue = GetJsonValue(value);
                    json.Append(indent + $"  \"{property.Name}\": {jsonValue}");
                    
                    if (currentMember < totalMembers)
                        json.Append(",");
                    
                    json.Append("\n");
                }
                catch
                {
                    // Skip properties that cannot be read
                }
            }

            json.Append(indent + "}");
            return json.ToString();
        }

        /// <summary>
        /// Converts a value to its JSON representation
        /// </summary>
        static string GetJsonValue(object value)
        {
            if (value == null)
                return "null";

            Type valueType = value.GetType();

            if (valueType == typeof(string))
                return $"\"{value}\"";
            else if (valueType == typeof(bool))
                return value.ToString().ToLower();
            else if (valueType.IsPrimitive)
                return value.ToString();
            else
                return ObjectToJson(value);
        }
    }

    // Sample classes
    class Student
    {
        public string Name { get; set; }
        public int RollNumber { get; set; }
        public double GPA { get; set; }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }

    class Employee
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
    }
}
