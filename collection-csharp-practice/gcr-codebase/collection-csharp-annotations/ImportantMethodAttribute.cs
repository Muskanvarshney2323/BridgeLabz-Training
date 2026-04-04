using System;
using System.Reflection;

namespace CollectionAnnotations.Beginner
{
    /// <summary>
    /// Beginner Level - Problem 1: Create an Attribute to Mark Important Methods
    /// Define a custom attribute ImportantMethod that can be applied to methods 
    /// to indicate their importance.
    /// </summary>

    // Custom ImportantMethod Attribute
    [AttributeUsage(AttributeTargets.Method)]
    public class ImportantMethodAttribute : Attribute
    {
        public string Level { get; set; }

        public ImportantMethodAttribute(string level = "HIGH")
        {
            Level = level;
        }

        public override string ToString()
        {
            return $"Importance Level: {Level}";
        }
    }

    class PaymentService
    {
        [ImportantMethod("CRITICAL")]
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing payment of ${amount}");
        }

        [ImportantMethod("HIGH")]
        public bool ValidatePaymentMethod(string cardNumber)
        {
            Console.WriteLine($"Validating payment method: {cardNumber}");
            return true;
        }

        [ImportantMethod("MEDIUM")]
        public void LogTransaction(string transactionId)
        {
            Console.WriteLine($"Logging transaction: {transactionId}");
        }

        [ImportantMethod()]  // Uses default "HIGH"
        public void SendReceipt(string email)
        {
            Console.WriteLine($"Sending receipt to: {email}");
        }

        public void PrintStatement()
        {
            Console.WriteLine("Printing statement...");
        }
    }

    class InventoryService
    {
        [ImportantMethod("CRITICAL")]
        public void UpdateStockLevel(string productId, int quantity)
        {
            Console.WriteLine($"Updating stock for {productId}: {quantity} units");
        }

        [ImportantMethod("HIGH")]
        public void AlertLowStock(string productId)
        {
            Console.WriteLine($"Alert: Low stock for {productId}");
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Displaying inventory...");
        }
    }

    class ImportantMethodAttributeDemo
    {
        static void Main()
        {
            Console.WriteLine("=== ImportantMethod Attribute - Beginner Level ===\n");

            // Example 1: Display important methods in PaymentService
            Console.WriteLine("Example 1: Important Methods in PaymentService\n");
            DisplayImportantMethods(typeof(PaymentService));

            // Example 2: Display important methods in InventoryService
            Console.WriteLine("\n\nExample 2: Important Methods in InventoryService\n");
            DisplayImportantMethods(typeof(InventoryService));

            // Example 3: Execute important methods only
            Console.WriteLine("\n\nExample 3: Execute Critical Methods\n");
            ExecuteMethodsByLevel(typeof(PaymentService), "CRITICAL");

            // Example 4: Filter by importance level
            Console.WriteLine("\n\nExample 4: All Methods Grouped by Importance\n");
            DisplayMethodsByImportance(typeof(PaymentService));
        }

        static void DisplayImportantMethods(Type type)
        {
            Console.WriteLine($"Important Methods in {type.Name}:\n");

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            int importantCount = 0;

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(ImportantMethodAttribute), false);

                if (attributes.Length > 0)
                {
                    importantCount++;
                    ImportantMethodAttribute attr = (ImportantMethodAttribute)attributes[0];
                    Console.WriteLine($"  {importantCount}. {method.Name}");
                    Console.WriteLine($"     {attr}");
                    Console.WriteLine();
                }
            }

            if (importantCount == 0)
            {
                Console.WriteLine("No important methods found.");
            }
        }

        static void ExecuteMethodsByLevel(Type type, string level)
        {
            Console.WriteLine($"Executing {level} priority methods:\n");

            object instance = Activator.CreateInstance(type);
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(ImportantMethodAttribute), false);

                if (attributes.Length > 0)
                {
                    ImportantMethodAttribute attr = (ImportantMethodAttribute)attributes[0];

                    if (attr.Level == level)
                    {
                        // Provide sample parameters
                        object[] parameters = GetSampleParameters(method);
                        Console.WriteLine($"Executing: {method.Name}");
                        try
                        {
                            method.Invoke(instance, parameters);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        static void DisplayMethodsByImportance(Type type)
        {
            Console.WriteLine($"Methods in {type.Name} Grouped by Importance:\n");

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            string[] levels = { "CRITICAL", "HIGH", "MEDIUM", "LOW" };

            foreach (string level in levels)
            {
                Console.WriteLine($"--- {level} ---");
                bool found = false;

                foreach (MethodInfo method in methods)
                {
                    object[] attributes = method.GetCustomAttributes(typeof(ImportantMethodAttribute), false);

                    if (attributes.Length > 0)
                    {
                        ImportantMethodAttribute attr = (ImportantMethodAttribute)attributes[0];
                        if (attr.Level == level)
                        {
                            Console.WriteLine($"  • {method.Name}");
                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("  (none)");
                }
                Console.WriteLine();
            }
        }

        static object[] GetSampleParameters(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] values = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(string))
                    values[i] = "sample";
                else if (parameters[i].ParameterType == typeof(double))
                    values[i] = 100.0;
                else if (parameters[i].ParameterType == typeof(int))
                    values[i] = 5;
                else if (parameters[i].ParameterType == typeof(bool))
                    values[i] = true;
            }

            return values;
        }
    }
}
