using System;
using System.Reflection;

namespace CollectionReflection.IntermediateLevel
{
    /// <summary>
    /// Problem 5: Dynamic Method Invocation
    /// Define a class MathOperations with multiple public methods (Add, Subtract, Multiply). 
    /// Use Reflection to dynamically call any method based on user input.
    /// </summary>
    class DynamicMethodInvocation
    {
        static void Main()
        {
            Console.WriteLine("=== Dynamic Method Invocation Using Reflection ===\n");

            MathOperations mathOps = new MathOperations();
            Type mathType = typeof(MathOperations);

            // Display available methods
            Console.WriteLine("Available Methods:");
            MethodInfo[] methods = mathType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"  - {method.Name}");
            }

            // Interactive method invocation
            Console.WriteLine("\n--- Dynamic Method Invocation ---");
            Console.Write("\nEnter method name (Add, Subtract, Multiply, Divide): ");
            string methodName = Console.ReadLine();

            Console.Write("Enter first number: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = int.Parse(Console.ReadLine());

            try
            {
                // Get the method dynamically
                MethodInfo method = mathType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (method != null)
                {
                    // Invoke the method
                    object result = method.Invoke(mathOps, new object[] { num1, num2 });
                    Console.WriteLine($"\n{methodName}({num1}, {num2}) = {result}");
                }
                else
                {
                    Console.WriteLine($"Method '{methodName}' not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }

            // Demonstrate all methods
            Console.WriteLine("\n--- All Operations ---");
            Console.WriteLine($"Add(10, 5) = {mathOps.Add(10, 5)}");
            Console.WriteLine($"Subtract(10, 5) = {mathOps.Subtract(10, 5)}");
            Console.WriteLine($"Multiply(10, 5) = {mathOps.Multiply(10, 5)}");
            Console.WriteLine($"Divide(10, 5) = {mathOps.Divide(10, 5)}");
        }
    }

    class MathOperations
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new ArgumentException("Cannot divide by zero");
            return a / b;
        }
    }
}
