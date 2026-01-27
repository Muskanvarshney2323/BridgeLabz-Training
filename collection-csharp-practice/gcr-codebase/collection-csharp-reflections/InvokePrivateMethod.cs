using System;
using System.Reflection;

namespace CollectionReflection.BasicLevel
{
    /// <summary>
    /// Problem 3: Invoke Private Method
    /// Define a class Calculator with a private method Multiply(int a, int b). 
    /// Use Reflection to invoke this method and display the result.
    /// </summary>
    class InvokePrivateMethod
    {
        static void Main()
        {
            Console.WriteLine("=== Invoke Private Method Using Reflection ===\n");

            Calculator calculator = new Calculator();

            // Get the private method using Reflection
            Type calculatorType = typeof(Calculator);
            MethodInfo multiplyMethod = calculatorType.GetMethod("Multiply", BindingFlags.NonPublic | BindingFlags.Instance);

            if (multiplyMethod != null)
            {
                // Invoke the private method
                int result = (int)multiplyMethod.Invoke(calculator, new object[] { 5, 6 });
                Console.WriteLine($"Multiply(5, 6) = {result}");

                // Try another set of numbers
                result = (int)multiplyMethod.Invoke(calculator, new object[] { 10, 20 });
                Console.WriteLine($"Multiply(10, 20) = {result}");

                // Try another set of numbers
                result = (int)multiplyMethod.Invoke(calculator, new object[] { 7, 8 });
                Console.WriteLine($"Multiply(7, 8) = {result}");
            }
            else
            {
                Console.WriteLine("Method 'Multiply' not found!");
            }

            // Also demonstrate invoking with dynamic parameters
            Console.WriteLine("\n--- Interactive Invocation ---");
            Console.Write("Enter first number: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write("Enter second number: ");
            int num2 = int.Parse(Console.ReadLine());

            if (multiplyMethod != null)
            {
                int result = (int)multiplyMethod.Invoke(calculator, new object[] { num1, num2 });
                Console.WriteLine($"Multiply({num1}, {num2}) = {result}");
            }
        }
    }

    class Calculator
    {
        // Private method
        private int Multiply(int a, int b)
        {
            return a * b;
        }

        // Public method for comparison
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
