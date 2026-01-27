using System;
using System.Diagnostics;
using System.Reflection;

namespace CollectionAnnotations.Intermediate
{
    /// <summary>
    /// Intermediate Level - Problem 3: Create an Attribute for Logging Method Execution Time
    /// Define an attribute LogExecutionTime to measure method execution time.
    /// </summary>

    // Custom LogExecutionTime Attribute
    [AttributeUsage(AttributeTargets.Method)]
    public class LogExecutionTimeAttribute : Attribute
    {
    }

    class DataProcessor
    {
        [LogExecutionTime]
        public void ProcessLargeDataset(int[] data)
        {
            System.Threading.Thread.Sleep(500); // Simulate processing
            Console.WriteLine($"Processing {data.Length} items...");
        }

        [LogExecutionTime]
        public int CalculateSum(int[] numbers)
        {
            System.Threading.Thread.Sleep(200);
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum;
        }

        [LogExecutionTime]
        public string SearchDatabase(string query)
        {
            System.Threading.Thread.Sleep(300);
            return $"Found results for: {query}";
        }

        [LogExecutionTime]
        public void SaveToFile(string filename)
        {
            System.Threading.Thread.Sleep(400);
            Console.WriteLine($"Saved to {filename}");
        }

        public void UnnmarkedMethod()
        {
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("This method is not marked for logging.");
        }
    }

    class LogExecutionTimeDemo
    {
        static void Main()
        {
            Console.WriteLine("=== LogExecutionTime Attribute - Intermediate Level ===\n");

            // Example 1: Invoke methods with execution time logging
            Console.WriteLine("Example 1: Logging Method Execution Time\n");
            LoggedInvokeMethods();

            // Example 2: Compare execution times of different methods
            Console.WriteLine("\n\nExample 2: Performance Comparison\n");
            CompareMethodPerformance();

            // Example 3: Interactive method invocation with logging
            Console.WriteLine("\n\nExample 3: Time All Methods\n");
            TimeAllAnnotatedMethods();
        }

        static void LoggedInvokeMethods()
        {
            DataProcessor processor = new DataProcessor();
            Type processorType = typeof(DataProcessor);

            MethodInfo[] methods = processorType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(LogExecutionTimeAttribute), false);

                if (attributes.Length > 0)
                {
                    Console.WriteLine($"Executing: {method.Name}");
                    
                    // Measure execution time
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    try
                    {
                        // Provide sample parameters
                        object[] parameters = GetSampleParameters(method);
                        object result = method.Invoke(processor, parameters);
                        
                        stopwatch.Stop();

                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.ElapsedTicks} ticks)");
                        
                        if (result != null)
                        {
                            Console.WriteLine($"Result: {result}");
                        }
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();
                        Console.WriteLine($"Error: {ex.InnerException?.Message}");
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds}ms");
                    }

                    Console.WriteLine();
                }
            }
        }

        static void CompareMethodPerformance()
        {
            DataProcessor processor = new DataProcessor();
            Type processorType = typeof(DataProcessor);

            Console.WriteLine("Method Performance Comparison:\n");
            Console.WriteLine("{0,-30} {1,-20} {2,-15}", "Method Name", "Execution Time", "Status");
            Console.WriteLine(new string('-', 65));

            MethodInfo[] methods = processorType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(LogExecutionTimeAttribute), false);

                if (attributes.Length > 0)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    try
                    {
                        object[] parameters = GetSampleParameters(method);
                        method.Invoke(processor, parameters);
                        stopwatch.Stop();

                        Console.WriteLine("{0,-30} {1,-20} {2,-15}", 
                            method.Name,
                            $"{stopwatch.ElapsedMilliseconds}ms",
                            "✓ Success");
                    }
                    catch
                    {
                        stopwatch.Stop();
                        Console.WriteLine("{0,-30} {1,-20} {2,-15}", 
                            method.Name,
                            $"{stopwatch.ElapsedMilliseconds}ms",
                            "✗ Error");
                    }
                }
            }
        }

        static void TimeAllAnnotatedMethods()
        {
            DataProcessor processor = new DataProcessor();
            Type processorType = typeof(DataProcessor);

            MethodInfo[] methods = processorType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            Console.WriteLine("Timing all annotated methods:\n");

            int count = 0;
            long totalTime = 0;
            double averageTime = 0;

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(LogExecutionTimeAttribute), false);

                if (attributes.Length > 0)
                {
                    count++;
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    try
                    {
                        object[] parameters = GetSampleParameters(method);
                        method.Invoke(processor, parameters);
                        stopwatch.Stop();

                        totalTime += stopwatch.ElapsedMilliseconds;
                        Console.WriteLine($"{count}. {method.Name}: {stopwatch.ElapsedMilliseconds}ms");
                    }
                    catch
                    {
                        stopwatch.Stop();
                        totalTime += stopwatch.ElapsedMilliseconds;
                        Console.WriteLine($"{count}. {method.Name}: {stopwatch.ElapsedMilliseconds}ms (Error)");
                    }
                }
            }

            if (count > 0)
            {
                averageTime = (double)totalTime / count;
                Console.WriteLine($"\n--- Summary ---");
                Console.WriteLine($"Total Methods: {count}");
                Console.WriteLine($"Total Time: {totalTime}ms");
                Console.WriteLine($"Average Time: {averageTime:F2}ms");
            }
        }

        static object[] GetSampleParameters(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] values = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(string))
                    values[i] = "sample_query";
                else if (parameters[i].ParameterType == typeof(int[]))
                    values[i] = new int[] { 1, 2, 3, 4, 5 };
                else if (parameters[i].ParameterType == typeof(int))
                    values[i] = 10;
            }

            return values;
        }
    }
}
