using System;
using System.Diagnostics;
using System.Reflection;

namespace CollectionReflection.AdvancedLevel
{
    /// <summary>
    /// Problem 12: Method Execution Timing
    /// Use Reflection to measure the execution time of methods in a given class dynamically.
    /// </summary>
    class MethodExecutionTiming
    {
        static void Main()
        {
            Console.WriteLine("=== Method Execution Timing Using Reflection ===\n");

            // Example 1: Time a simple method
            Console.WriteLine("Example 1: Timing Simple Methods\n");
            Calculator calculator = new Calculator();
            
            MeasureMethodExecution(calculator, "SlowAdd", new object[] { 5, 10 });
            MeasureMethodExecution(calculator, "FastMultiply", new object[] { 5, 10 });
            MeasureMethodExecution(calculator, "SumArray", new object[] { new int[] { 1, 2, 3, 4, 5 } });

            // Example 2: Time all methods in a class
            Console.WriteLine("\n\nExample 2: Timing All Methods in Calculator Class\n");
            TimeAllMethods(calculator);

            // Example 3: Interactive method timing
            Console.WriteLine("\n\nExample 3: Interactive Method Timing\n");
            InteractiveMethodTiming();

            // Example 4: Compare method performance
            Console.WriteLine("\n\nExample 4: Performance Comparison\n");
            ComparePerformance();
        }

        /// <summary>
        /// Measures the execution time of a specific method
        /// </summary>
        static void MeasureMethodExecution(object obj, string methodName, object[] parameters = null)
        {
            Type objType = obj.GetType();
            MethodInfo method = objType.GetMethod(methodName);

            if (method == null)
            {
                Console.WriteLine($"Method '{methodName}' not found!");
                return;
            }

            // Create a stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                // Invoke the method
                object result = method.Invoke(obj, parameters ?? new object[] { });
                
                // Stop the stopwatch
                stopwatch.Stop();

                // Display results
                Console.WriteLine($"Method: {methodName}");
                Console.WriteLine($"Result: {result}");
                Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.ElapsedTicks} ticks)");
                Console.WriteLine($"Average: {stopwatch.ElapsedTicks / 1000.0}μs\n");
            }
            catch (TargetInvocationException ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"Method: {methodName}");
                Console.WriteLine($"Exception: {ex.InnerException?.Message}");
                Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds}ms\n");
            }
        }

        /// <summary>
        /// Times all public methods in a class
        /// </summary>
        static void TimeAllMethods(object obj)
        {
            Type objType = obj.GetType();
            MethodInfo[] methods = objType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            Console.WriteLine($"Timing all methods in {objType.Name}:\n");

            foreach (MethodInfo method in methods)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                try
                {
                    // Call method with default parameters
                    ParameterInfo[] parameters = method.GetParameters();
                    object[] paramValues = new object[parameters.Length];

                    // Try to provide default values for parameters
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].ParameterType == typeof(int))
                            paramValues[i] = 5;
                        else if (parameters[i].ParameterType == typeof(string))
                            paramValues[i] = "test";
                        else if (parameters[i].ParameterType == typeof(int[]))
                            paramValues[i] = new int[] { 1, 2, 3 };
                    }

                    object result = method.Invoke(obj, paramValues);
                    stopwatch.Stop();

                    Console.WriteLine($"  {method.Name}: {stopwatch.ElapsedMilliseconds}ms");
                }
                catch
                {
                    stopwatch.Stop();
                    Console.WriteLine($"  {method.Name}: Error during execution");
                }
            }
        }

        /// <summary>
        /// Interactive method timing where user can input method name
        /// </summary>
        static void InteractiveMethodTiming()
        {
            Console.Write("Enter class name (Calculator): ");
            string className = Console.ReadLine() ?? "Calculator";

            // Create instance based on class name
            Type type = Type.GetType($"CollectionReflection.AdvancedLevel.{className}");
            if (type == null)
            {
                Console.WriteLine("Class not found!");
                return;
            }

            object instance = Activator.CreateInstance(type);

            Console.Write("Enter method name: ");
            string methodName = Console.ReadLine();

            MeasureMethodExecution(instance, methodName);
        }

        /// <summary>
        /// Compare performance of different methods
        /// </summary>
        static void ComparePerformance()
        {
            Calculator calculator = new Calculator();
            const int iterations = 1000;

            Console.WriteLine($"Performance Comparison ({iterations} iterations):\n");

            // Time SlowAdd
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                calculator.SlowAdd(5, 10);
            }
            stopwatch.Stop();
            Console.WriteLine($"SlowAdd: {stopwatch.ElapsedMilliseconds}ms");

            // Time FastMultiply
            stopwatch.Restart();
            for (int i = 0; i < iterations; i++)
            {
                calculator.FastMultiply(5, 10);
            }
            stopwatch.Stop();
            Console.WriteLine($"FastMultiply: {stopwatch.ElapsedMilliseconds}ms");

            // Calculate and display performance difference
            Console.WriteLine($"\nFastMultiply is faster than SlowAdd");
        }
    }

    // Sample class with various methods
    class Calculator
    {
        public int SlowAdd(int a, int b)
        {
            // Simulate slow operation
            System.Threading.Thread.Sleep(1);
            return a + b;
        }

        public int FastMultiply(int a, int b)
        {
            return a * b;
        }

        public int SumArray(int[] numbers)
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum;
        }

        public string Concatenate(string a, string b)
        {
            return a + b;
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public double CalculateAverage(int[] numbers)
        {
            if (numbers.Length == 0)
                return 0;

            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return (double)sum / numbers.Length;
        }
    }
}
