using System;
using System.Collections.Generic;
using System.Reflection;

namespace CollectionAnnotations.Beginner
{
    /// <summary>
    /// Beginner Level - Problem 2: Create a Todo Attribute for Pending Tasks
    /// Define an attribute Todo to mark pending features in a project.
    /// </summary>

    // Custom Todo Attribute
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class TodoAttribute : Attribute
    {
        public string Task { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }

        public TodoAttribute(string task, string assignedTo, string priority = "MEDIUM")
        {
            Task = task;
            AssignedTo = assignedTo;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"Task: {Task} | Assigned: {AssignedTo} | Priority: {Priority}";
        }
    }

    [Todo("Implement user registration", "Alice", "HIGH")]
    class UserModule
    {
        [Todo("Add email verification", "John", "HIGH")]
        [Todo("Implement password reset", "Jane", "MEDIUM")]
        public void RegisterUser(string email)
        {
            Console.WriteLine($"Registering user with email: {email}");
        }

        [Todo("Add two-factor authentication", "Bob", "HIGH")]
        public void AuthenticateUser(string username)
        {
            Console.WriteLine($"Authenticating user: {username}");
        }

        [Todo("Improve profile UI", "Alice", "LOW")]
        public void UpdateProfile(string userId)
        {
            Console.WriteLine($"Updating profile for user: {userId}");
        }
    }

    [Todo("Implement payment processing", "Payment Team", "CRITICAL")]
    class PaymentModule
    {
        [Todo("Add credit card validation", "Tom", "HIGH")]
        [Todo("Implement PCI DSS compliance", "Security Team", "CRITICAL")]
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing payment: ${amount}");
        }

        [Todo("Add invoice generation", "Excel Expert", "MEDIUM")]
        public void GenerateInvoice(string orderId)
        {
            Console.WriteLine($"Generating invoice for order: {orderId}");
        }
    }

    class TodoAttributeDemo
    {
        static void Main()
        {
            Console.WriteLine("=== Todo Attribute - Beginner Level ===\n");

            // Example 1: Display all pending tasks
            Console.WriteLine("Example 1: All Pending Tasks\n");
            DisplayAllPendingTasks();

            // Example 2: Display tasks by priority
            Console.WriteLine("\n\nExample 2: Tasks Grouped by Priority\n");
            DisplayTasksByPriority();

            // Example 3: Display tasks by assignee
            Console.WriteLine("\n\nExample 3: Tasks Grouped by Assignee\n");
            DisplayTasksByAssignee();

            // Example 4: Count pending tasks
            Console.WriteLine("\n\nExample 4: Task Statistics\n");
            DisplayTaskStatistics();
        }

        static void DisplayAllPendingTasks()
        {
            Type[] types = { typeof(UserModule), typeof(PaymentModule) };

            foreach (Type type in types)
            {
                Console.WriteLine($"--- {type.Name} ---");
                
                // Class-level todos
                object[] classAttributes = type.GetCustomAttributes(typeof(TodoAttribute), false);
                if (classAttributes.Length > 0)
                {
                    Console.WriteLine("Class Tasks:");
                    foreach (TodoAttribute todo in classAttributes)
                    {
                        Console.WriteLine($"  • {todo}");
                    }
                    Console.WriteLine();
                }

                // Method-level todos
                MethodInfo[] methods = type.GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly);

                bool foundMethodTodos = false;
                foreach (MethodInfo method in methods)
                {
                    object[] attributes = method.GetCustomAttributes(typeof(TodoAttribute), false);
                    if (attributes.Length > 0)
                    {
                        if (!foundMethodTodos)
                        {
                            Console.WriteLine("Method Tasks:");
                            foundMethodTodos = true;
                        }

                        Console.WriteLine($"  {method.Name}:");
                        foreach (TodoAttribute todo in attributes)
                        {
                            Console.WriteLine($"    • {todo}");
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        static void DisplayTasksByPriority()
        {
            Type[] types = { typeof(UserModule), typeof(PaymentModule) };
            string[] priorities = { "CRITICAL", "HIGH", "MEDIUM", "LOW" };

            foreach (string priority in priorities)
            {
                Console.WriteLine($"--- {priority} Priority ---");

                int count = 0;
                foreach (Type type in types)
                {
                    // Check class-level attributes
                    object[] classAttributes = type.GetCustomAttributes(typeof(TodoAttribute), false);
                    foreach (TodoAttribute todo in classAttributes)
                    {
                        if (todo.Priority == priority)
                        {
                            Console.WriteLine($"  [{type.Name}] {todo.Task}");
                            Console.WriteLine($"    Assigned: {todo.AssignedTo}\n");
                            count++;
                        }
                    }

                    // Check method-level attributes
                    MethodInfo[] methods = type.GetMethods(
                        BindingFlags.Public | 
                        BindingFlags.Instance | 
                        BindingFlags.DeclaredOnly);

                    foreach (MethodInfo method in methods)
                    {
                        object[] attributes = method.GetCustomAttributes(typeof(TodoAttribute), false);
                        foreach (TodoAttribute todo in attributes)
                        {
                            if (todo.Priority == priority)
                            {
                                Console.WriteLine($"  [{type.Name}.{method.Name}] {todo.Task}");
                                Console.WriteLine($"    Assigned: {todo.AssignedTo}\n");
                                count++;
                            }
                        }
                    }
                }

                if (count == 0)
                {
                    Console.WriteLine("  (none)\n");
                }
            }
        }

        static void DisplayTasksByAssignee()
        {
            Type[] types = { typeof(UserModule), typeof(PaymentModule) };
            Dictionary<string, List<string>> tasksByAssignee = new Dictionary<string, List<string>>();

            // Collect all tasks
            foreach (Type type in types)
            {
                // Class-level attributes
                object[] classAttributes = type.GetCustomAttributes(typeof(TodoAttribute), false);
                foreach (TodoAttribute todo in classAttributes)
                {
                    if (!tasksByAssignee.ContainsKey(todo.AssignedTo))
                        tasksByAssignee[todo.AssignedTo] = new List<string>();
                    tasksByAssignee[todo.AssignedTo].Add(todo.Task);
                }

                // Method-level attributes
                MethodInfo[] methods = type.GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly);

                foreach (MethodInfo method in methods)
                {
                    object[] attributes = method.GetCustomAttributes(typeof(TodoAttribute), false);
                    foreach (TodoAttribute todo in attributes)
                    {
                        if (!tasksByAssignee.ContainsKey(todo.AssignedTo))
                            tasksByAssignee[todo.AssignedTo] = new List<string>();
                        tasksByAssignee[todo.AssignedTo].Add(todo.Task);
                    }
                }
            }

            // Display by assignee
            foreach (var kvp in tasksByAssignee)
            {
                Console.WriteLine($"--- {kvp.Key} ({kvp.Value.Count} tasks) ---");
                foreach (string task in kvp.Value)
                {
                    Console.WriteLine($"  • {task}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayTaskStatistics()
        {
            Type[] types = { typeof(UserModule), typeof(PaymentModule) };

            int totalTasks = 0;
            int criticalCount = 0;
            int highCount = 0;
            int mediumCount = 0;
            int lowCount = 0;

            foreach (Type type in types)
            {
                // Class attributes
                object[] classAttributes = type.GetCustomAttributes(typeof(TodoAttribute), false);
                foreach (TodoAttribute todo in classAttributes)
                {
                    totalTasks++;
                    CountByPriority(todo.Priority, ref criticalCount, ref highCount, ref mediumCount, ref lowCount);
                }

                // Method attributes
                MethodInfo[] methods = type.GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly);

                foreach (MethodInfo method in methods)
                {
                    object[] attributes = method.GetCustomAttributes(typeof(TodoAttribute), false);
                    foreach (TodoAttribute todo in attributes)
                    {
                        totalTasks++;
                        CountByPriority(todo.Priority, ref criticalCount, ref highCount, ref mediumCount, ref lowCount);
                    }
                }
            }

            Console.WriteLine($"Total Pending Tasks: {totalTasks}\n");
            Console.WriteLine($"CRITICAL: {criticalCount}");
            Console.WriteLine($"HIGH: {highCount}");
            Console.WriteLine($"MEDIUM: {mediumCount}");
            Console.WriteLine($"LOW: {lowCount}");
        }

        static void CountByPriority(string priority, ref int critical, ref int high, ref int medium, ref int low)
        {
            switch (priority)
            {
                case "CRITICAL":
                    critical++;
                    break;
                case "HIGH":
                    high++;
                    break;
                case "MEDIUM":
                    medium++;
                    break;
                case "LOW":
                    low++;
                    break;
            }
        }
    }
}
