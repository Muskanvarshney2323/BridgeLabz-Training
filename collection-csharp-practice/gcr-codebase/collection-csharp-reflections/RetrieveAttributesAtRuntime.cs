using System;
using System.Reflection;

namespace CollectionReflection.IntermediateLevel
{
    /// <summary>
    /// Problem 6: Retrieve Attributes at Runtime
    /// Create a custom attribute [Author("Author Name")]. Apply it to a class 
    /// and use Reflection to retrieve and display the attribute value at runtime.
    /// </summary>
    /// 
    // Custom Attribute Definition
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorAttribute : Attribute
    {
        public string Name { get; set; }
        public string Date { get; set; }

        public AuthorAttribute(string name)
        {
            Name = name;
            Date = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public AuthorAttribute(string name, string date)
        {
            Name = name;
            Date = date;
        }
    }

    // Apply the custom attribute to classes and methods
    [Author("John Developer")]
    class CalculatorApp
    {
        [Author("Alice Designer", "2024-01-15")]
        public void PerformCalculations()
        {
            Console.WriteLine("Performing calculations...");
        }

        public void DisplayData()
        {
            Console.WriteLine("Displaying data...");
        }
    }

    [Author("Bob Tester", "2024-02-20")]
    class TestUtilities
    {
        public void RunTests()
        {
            Console.WriteLine("Running tests...");
        }
    }

    class RetrieveAttributesAtRuntime
    {
        static void Main()
        {
            Console.WriteLine("=== Retrieve Custom Attributes at Runtime ===\n");

            // Get attributes from CalculatorApp class
            Type calcType = typeof(CalculatorApp);
            Console.WriteLine($"Class: {calcType.Name}");
            
            // Get class-level attributes
            object[] classAttributes = calcType.GetCustomAttributes(typeof(AuthorAttribute), true);
            if (classAttributes.Length > 0)
            {
                AuthorAttribute attr = (AuthorAttribute)classAttributes[0];
                Console.WriteLine($"Author: {attr.Name}, Date: {attr.Date}\n");
            }

            // Get method-level attributes
            Console.WriteLine("Methods and their Authors:");
            MethodInfo[] methods = calcType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                object[] methodAttributes = method.GetCustomAttributes(typeof(AuthorAttribute), true);
                Console.Write($"  - {method.Name}: ");
                if (methodAttributes.Length > 0)
                {
                    AuthorAttribute attr = (AuthorAttribute)methodAttributes[0];
                    Console.WriteLine($"Author: {attr.Name}, Date: {attr.Date}");
                }
                else
                {
                    Console.WriteLine("No author attribute");
                }
            }

            // Get attributes from TestUtilities class
            Console.WriteLine($"\n\nClass: TestUtilities");
            Type testType = typeof(TestUtilities);
            classAttributes = testType.GetCustomAttributes(typeof(AuthorAttribute), true);
            if (classAttributes.Length > 0)
            {
                AuthorAttribute attr = (AuthorAttribute)classAttributes[0];
                Console.WriteLine($"Author: {attr.Name}, Date: {attr.Date}\n");
            }

            // Generic method to retrieve attributes from any type
            Console.WriteLine("--- Using Generic Method ---");
            RetrieveAllAttributes(calcType);
            RetrieveAllAttributes(testType);
        }

        // Generic method to retrieve and display all Author attributes
        static void RetrieveAllAttributes(Type type)
        {
            Console.WriteLine($"\nClass: {type.Name}");
            object[] attributes = type.GetCustomAttributes(typeof(AuthorAttribute), true);
            
            foreach (AuthorAttribute attr in attributes)
            {
                Console.WriteLine($"  Author: {attr.Name}");
                Console.WriteLine($"  Date: {attr.Date}");
            }
        }
    }
}
