using System;
using System.Reflection;

namespace CollectionAnnotations.Exercises
{
    /// <summary>
    /// Exercise 4: Create a Custom Attribute and Use It
    /// Create a custom attribute TaskInfo to mark tasks with priority and assigned person.
    /// Apply this attribute to methods and retrieve details using Reflection.
    /// </summary>
    /// 
    // Custom TaskInfo Attribute
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class TaskInfoAttribute : Attribute
    {
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }

        public TaskInfoAttribute(string priority, string assignedTo)
        {
            Priority = priority;
            AssignedTo = assignedTo;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return $"Priority: {Priority}, Assigned To: {AssignedTo}, Description: {Description}";
        }
    }

    // TaskManager class with TaskInfo attributes
    [TaskInfo("HIGH", "Project Manager")]
    class TaskManager
    {
        [TaskInfo("HIGH", "John Developer")]
        public void ImplementNewFeature()
        {
            Console.WriteLine("Implementing new feature...");
        }

        [TaskInfo("MEDIUM", "Alice Tester")]
        public void WriteUnitTests()
        {
            Console.WriteLine("Writing unit tests...");
        }

        [TaskInfo("LOW", "Bob Maintainer")]
        public void UpdateDocumentation()
        {
            Console.WriteLine("Updating documentation...");
        }

        [TaskInfo("HIGH", "Security Team")]
        public void FixSecurityBug()
        {
            Console.WriteLine("Fixing security vulnerability...");
        }

        public void RegularMethod()
        {
            Console.WriteLine("Regular method without task info.");
        }
    }

    class CustomAttributeUsage
    {
        static void Main()
        {
            Console.WriteLine("=== Custom TaskInfo Attribute Usage ===\n");

            // Example 1: Retrieve class-level attributes
            Console.WriteLine("Example 1: Class-Level Attribute\n");
            RetrieveClassTaskInfo();

            // Example 2: Retrieve method-level attributes
            Console.WriteLine("\n\nExample 2: Method-Level Attributes\n");
            RetrieveMethodTaskInfo();

            // Example 3: Execute methods based on priority
            Console.WriteLine("\n\nExample 3: Execute Methods by Priority\n");
            ExecuteMethodsByPriority("HIGH");

            // Example 4: Display all tasks
            Console.WriteLine("\n\nExample 4: All Tasks\n");
            DisplayAllTasks();
        }

        static void RetrieveClassTaskInfo()
        {
            Type taskManagerType = typeof(TaskManager);
            object[] attributes = taskManagerType.GetCustomAttributes(typeof(TaskInfoAttribute), false);

            if (attributes.Length > 0)
            {
                TaskInfoAttribute attr = (TaskInfoAttribute)attributes[0];
                Console.WriteLine($"Class: {taskManagerType.Name}");
                Console.WriteLine($"Task Info: {attr}");
            }
        }

        static void RetrieveMethodTaskInfo()
        {
            Type taskManagerType = typeof(TaskManager);
            MethodInfo[] methods = taskManagerType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(TaskInfoAttribute), false);

                if (attributes.Length > 0)
                {
                    TaskInfoAttribute attr = (TaskInfoAttribute)attributes[0];
                    Console.WriteLine($"Method: {method.Name}");
                    Console.WriteLine($"  {attr}\n");
                }
            }
        }

        static void ExecuteMethodsByPriority(string priority)
        {
            TaskManager taskManager = new TaskManager();
            Type taskManagerType = typeof(TaskManager);
            MethodInfo[] methods = taskManagerType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            Console.WriteLine($"Executing methods with {priority} priority:\n");

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(TaskInfoAttribute), false);

                if (attributes.Length > 0)
                {
                    TaskInfoAttribute attr = (TaskInfoAttribute)attributes[0];
                    
                    if (attr.Priority == priority)
                    {
                        Console.WriteLine($"Executing: {method.Name}");
                        method.Invoke(taskManager, null);
                        Console.WriteLine();
                    }
                }
            }
        }

        static void DisplayAllTasks()
        {
            Type taskManagerType = typeof(TaskManager);
            MethodInfo[] methods = taskManagerType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            Console.WriteLine("All Tasks in TaskManager:\n");
            int taskCount = 0;

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(TaskInfoAttribute), false);

                if (attributes.Length > 0)
                {
                    taskCount++;
                    TaskInfoAttribute attr = (TaskInfoAttribute)attributes[0];
                    Console.WriteLine($"Task {taskCount}: {method.Name}");
                    Console.WriteLine($"  Priority: {attr.Priority}");
                    Console.WriteLine($"  Assigned To: {attr.AssignedTo}");
                    Console.WriteLine();
                }
            }

            if (taskCount == 0)
            {
                Console.WriteLine("No tasks found.");
            }
        }
    }
}
