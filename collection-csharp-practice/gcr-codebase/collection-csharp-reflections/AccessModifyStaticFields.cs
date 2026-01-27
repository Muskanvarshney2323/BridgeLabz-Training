using System;
using System.Reflection;

namespace CollectionReflection.IntermediateLevel
{
    /// <summary>
    /// Problem 7: Access and Modify Static Fields
    /// Create a Configuration class with a private static field API_KEY. 
    /// Use Reflection to modify its value and print it.
    /// </summary>
    class AccessModifyStaticFields
    {
        static void Main()
        {
            Console.WriteLine("=== Access and Modify Static Fields Using Reflection ===\n");

            Type configType = typeof(Configuration);

            // Get the private static field
            FieldInfo apiKeyField = configType.GetField("API_KEY", BindingFlags.NonPublic | BindingFlags.Static);

            if (apiKeyField != null)
            {
                // Retrieve the current value
                object currentValue = apiKeyField.GetValue(null);
                Console.WriteLine($"Original API_KEY: {currentValue}");

                // Modify the static field value
                apiKeyField.SetValue(null, "NEW_API_KEY_12345");
                Console.WriteLine($"Modified API_KEY: {apiKeyField.GetValue(null)}\n");

                // Verify through property
                Console.WriteLine($"API_KEY via property: {Configuration.GetApiKey()}\n");

                // Demonstrate with another static field
                FieldInfo timeoutField = configType.GetField("TIMEOUT", BindingFlags.NonPublic | BindingFlags.Static);
                if (timeoutField != null)
                {
                    Console.WriteLine($"Original TIMEOUT: {timeoutField.GetValue(null)}");
                    timeoutField.SetValue(null, 15000);
                    Console.WriteLine($"Modified TIMEOUT: {timeoutField.GetValue(null)}\n");
                }
            }
            else
            {
                Console.WriteLine("Field 'API_KEY' not found!");
            }

            // Interactive modification
            Console.WriteLine("--- Interactive Static Field Modification ---");
            Console.Write("Enter new API_KEY: ");
            string newApiKey = Console.ReadLine();

            if (apiKeyField != null)
            {
                apiKeyField.SetValue(null, newApiKey);
                Console.WriteLine($"API_KEY updated to: {apiKeyField.GetValue(null)}");
            }

            // Display all static fields
            Console.WriteLine("\n--- All Static Fields ---");
            FieldInfo[] staticFields = configType.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (FieldInfo field in staticFields)
            {
                Console.WriteLine($"  {field.Name} = {field.GetValue(null)}");
            }
        }
    }

    class Configuration
    {
        private static string API_KEY = "DEFAULT_API_KEY";
        private static int TIMEOUT = 5000;
        private static string DATABASE_URL = "localhost:3306";

        public static string GetApiKey()
        {
            return API_KEY;
        }

        public static int GetTimeout()
        {
            return TIMEOUT;
        }

        public static string GetDatabaseUrl()
        {
            return DATABASE_URL;
        }
    }
}
