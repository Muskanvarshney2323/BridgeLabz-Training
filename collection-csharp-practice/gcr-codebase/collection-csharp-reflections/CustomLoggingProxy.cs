using System;
using System.Reflection;

namespace CollectionReflection.AdvancedLevel
{
    /// <summary>
    /// Problem 10: Custom Logging Proxy Using Reflection
    /// Implement a Dynamic Proxy that intercepts method calls on an interface 
    /// (e.g., IGreeting.SayHello()) and logs the method name before executing it.
    /// </summary>
    /// 
    // Define an interface
    interface IGreeting
    {
        void SayHello(string name);
        string GetGreeting(string name);
        void Farewell();
    }

    interface ICalculator
    {
        int Add(int a, int b);
        int Subtract(int a, int b);
        int Multiply(int a, int b);
    }

    // Implementation of IGreeting
    class GreetingService : IGreeting
    {
        public void SayHello(string name)
        {
            Console.WriteLine($"Hello, {name}! Welcome!");
        }

        public string GetGreeting(string name)
        {
            return $"Greetings, {name}!";
        }

        public void Farewell()
        {
            Console.WriteLine("Goodbye!");
        }
    }

    // Implementation of ICalculator
    class CalculatorService : ICalculator
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
    }

    // Logging Proxy using Reflection
    class LoggingProxy
    {
        public static T CreateProxy<T>(T target) where T : class
        {
            Type interfaceType = typeof(T);
            
            return (T)Activator.CreateInstance(
                typeof(DynamicProxy<>).MakeGenericType(interfaceType),
                target
            );
        }
    }

    // Generic Dynamic Proxy Handler
    class DynamicProxy<T> : RealProxy<T> where T : class
    {
        private T _target;

        public DynamicProxy(T target) : base(target)
        {
            _target = target;
        }

        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            // Log method invocation
            Console.WriteLine($"[LOG] Method called: {method.Name}");
            
            if (args.Length > 0)
            {
                Console.Write("[LOG] Parameters: ");
                foreach (var arg in args)
                {
                    Console.Write($"{arg}, ");
                }
                Console.WriteLine();
            }

            try
            {
                // Invoke the actual method
                object result = method.Invoke(_target, args);
                
                // Log result
                if (result != null)
                {
                    Console.WriteLine($"[LOG] Return value: {result}");
                }
                
                return result;
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine($"[LOG] Exception occurred: {ex.InnerException?.Message}");
                throw ex.InnerException;
            }
        }
    }

    // Base class for Real Proxy pattern
    abstract class RealProxy<T> where T : class
    {
        protected T Target { get; set; }

        public RealProxy(T target)
        {
            Target = target;
        }

        public abstract object Invoke(object proxy, MethodInfo method, object[] args);
    }

    // Alternative implementation using direct proxy
    class SimpleLoggingProxy
    {
        public static void InvokeWithLogging(object target, string methodName, object[] args = null)
        {
            Type targetType = target.GetType();
            MethodInfo method = targetType.GetMethod(methodName);

            if (method != null)
            {
                Console.WriteLine($"[LOG] Invoking method: {methodName}");
                
                if (args != null)
                {
                    Console.Write("[LOG] Parameters: ");
                    foreach (var arg in args)
                    {
                        Console.Write($"{arg}, ");
                    }
                    Console.WriteLine();
                }

                try
                {
                    object result = method.Invoke(target, args ?? new object[] { });
                    
                    if (result != null)
                    {
                        Console.WriteLine($"[LOG] Return value: {result}");
                    }
                }
                catch (TargetInvocationException ex)
                {
                    Console.WriteLine($"[LOG] Exception: {ex.InnerException?.Message}");
                }
            }
            else
            {
                Console.WriteLine($"[LOG] Method not found: {methodName}");
            }
        }
    }

    class CustomLoggingProxy
    {
        static void Main()
        {
            Console.WriteLine("=== Custom Logging Proxy Using Reflection ===\n");

            // Example 1: Simple logging approach
            Console.WriteLine("Example 1: Simple Logging Proxy\n");
            IGreeting greetingService = new GreetingService();
            
            SimpleLoggingProxy.InvokeWithLogging(greetingService, "SayHello", new object[] { "John" });
            Console.WriteLine();
            
            SimpleLoggingProxy.InvokeWithLogging(greetingService, "GetGreeting", new object[] { "Alice" });
            Console.WriteLine();
            
            SimpleLoggingProxy.InvokeWithLogging(greetingService, "Farewell");

            // Example 2: Calculator service logging
            Console.WriteLine("\n\nExample 2: Calculator Service Logging\n");
            ICalculator calculatorService = new CalculatorService();
            
            SimpleLoggingProxy.InvokeWithLogging(calculatorService, "Add", new object[] { 10, 20 });
            Console.WriteLine();
            
            SimpleLoggingProxy.InvokeWithLogging(calculatorService, "Multiply", new object[] { 5, 6 });
            Console.WriteLine();
            
            SimpleLoggingProxy.InvokeWithLogging(calculatorService, "Subtract", new object[] { 100, 30 });
        }
    }
}
