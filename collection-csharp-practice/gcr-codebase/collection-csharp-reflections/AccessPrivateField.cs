using System;
using System.Reflection;

namespace CollectionReflection.BasicLevel
{
    /// <summary>
    /// Problem 2: Access Private Field
    /// Create a class Person with a private field age. Use Reflection to modify 
    /// and retrieve its value.
    /// </summary>
    class AccessPrivateField
    {
        static void Main()
        {
            Console.WriteLine("=== Access Private Field Using Reflection ===\n");

            // Create instance of Person
            Person person = new Person("John", 25);

            // Display initial state (using public property)
            Console.WriteLine($"Name: {person.Name}");
            Console.WriteLine($"Age (via property): {person.AgeProperty}\n");

            // Get the private field using Reflection
            Type personType = typeof(Person);
            FieldInfo ageField = personType.GetField("age", BindingFlags.NonPublic | BindingFlags.Instance);

            if (ageField != null)
            {
                // Retrieve the private field value
                object ageValue = ageField.GetValue(person);
                Console.WriteLine($"Age (via Reflection - Retrieved): {ageValue}");

                // Modify the private field value
                ageField.SetValue(person, 30);
                Console.WriteLine($"Age (after modification via Reflection): {ageField.GetValue(person)}");

                // Verify the change through property
                Console.WriteLine($"Age (via property after modification): {person.AgeProperty}");
            }
            else
            {
                Console.WriteLine("Field 'age' not found!");
            }
        }
    }

    class Person
    {
        public string Name { get; set; }
        private int age;

        public int AgeProperty
        {
            get { return age; }
            set { age = value; }
        }

        public Person(string name, int age)
        {
            Name = name;
            this.age = age;
        }
    }
}
