using System;
using System.Reflection;

namespace CollectionAnnotations.Exercises
{
    /// <summary>
    /// Exercise 5: Create and Use a Repeatable Attribute
    /// Define an attribute BugReport that can be applied multiple times on a method.
    /// Use AllowMultiple = true to allow multiple bug reports.
    /// </summary>
    /// 
    // Repeatable BugReport Attribute
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BugReportAttribute : Attribute
    {
        public string Description { get; set; }
        public string BugId { get; set; }
        public string Severity { get; set; }

        public BugReportAttribute(string bugId, string description)
        {
            BugId = bugId;
            Description = description;
            Severity = "MEDIUM"; // Default severity
        }

        public override string ToString()
        {
            return $"[{BugId}] {Description} (Severity: {Severity})";
        }
    }

    class BugTrackingSystem
    {
        // Method with multiple bug reports
        [BugReport("BUG-001", "NullReferenceException when user input is empty")]
        [BugReport("BUG-002", "Performance issue on large dataset processing")]
        [BugReport("BUG-003", "Memory leak in data caching mechanism")]
        public void ProcessUserData(string data)
        {
            Console.WriteLine($"Processing data: {data}");
        }

        // Method with two bug reports
        [BugReport("BUG-004", "Login timeout not working properly")]
        [BugReport("BUG-005", "Session expiration message not displayed")]
        public void AuthenticateUser(string username)
        {
            Console.WriteLine($"Authenticating user: {username}");
        }

        // Method with single bug report
        [BugReport("BUG-006", "Report generation takes too long")]
        public void GenerateReport()
        {
            Console.WriteLine("Generating report...");
        }

        // Method without any bug reports
        public void NormalMethod()
        {
            Console.WriteLine("This method has no bugs reported.");
        }
    }

    class RepeatableAttributeUsage
    {
        static void Main()
        {
            Console.WriteLine("=== Repeatable BugReport Attribute Usage ===\n");

            // Example 1: Retrieve all bug reports for a specific method
            Console.WriteLine("Example 1: Bug Reports for ProcessUserData Method\n");
            RetrieveBugReportsForMethod("ProcessUserData");

            // Example 2: Retrieve all bug reports for all methods
            Console.WriteLine("\n\nExample 2: All Bug Reports\n");
            RetrieveAllBugReports();

            // Example 3: Count bugs by method
            Console.WriteLine("\n\nExample 3: Bug Count by Method\n");
            CountBugsByMethod();

            // Example 4: Execute method and display associated bugs
            Console.WriteLine("\n\nExample 4: Execute Method with Bug Info\n");
            ExecuteMethodWithBugInfo("AuthenticateUser", "testuser");
        }

        static void RetrieveBugReportsForMethod(string methodName)
        {
            Type bugTrackingType = typeof(BugTrackingSystem);
            MethodInfo method = bugTrackingType.GetMethod(methodName);

            if (method != null)
            {
                object[] attributes = method.GetCustomAttributes(typeof(BugReportAttribute), false);

                if (attributes.Length > 0)
                {
                    Console.WriteLine($"Method: {methodName}");
                    Console.WriteLine($"Total Bugs: {attributes.Length}\n");

                    for (int i = 0; i < attributes.Length; i++)
                    {
                        BugReportAttribute bugReport = (BugReportAttribute)attributes[i];
                        Console.WriteLine($"  Bug {i + 1}: {bugReport}");
                    }
                }
                else
                {
                    Console.WriteLine($"Method: {methodName}");
                    Console.WriteLine("No bugs reported for this method.");
                }
            }
            else
            {
                Console.WriteLine($"Method '{methodName}' not found.");
            }
        }

        static void RetrieveAllBugReports()
        {
            Type bugTrackingType = typeof(BugTrackingSystem);
            MethodInfo[] methods = bugTrackingType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            int totalBugs = 0;

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(BugReportAttribute), false);

                if (attributes.Length > 0)
                {
                    Console.WriteLine($"Method: {method.Name}");
                    Console.WriteLine($"Bugs: {attributes.Length}\n");

                    foreach (BugReportAttribute bugReport in attributes)
                    {
                        Console.WriteLine($"  • {bugReport}");
                        totalBugs++;
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"--- Total Bugs Reported: {totalBugs} ---");
        }

        static void CountBugsByMethod()
        {
            Type bugTrackingType = typeof(BugTrackingSystem);
            MethodInfo[] methods = bugTrackingType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            Console.WriteLine("Bug Count Summary:\n");

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(BugReportAttribute), false);
                Console.WriteLine($"{method.Name}: {attributes.Length} bug(s)");
            }
        }

        static void ExecuteMethodWithBugInfo(string methodName, params object[] parameters)
        {
            Type bugTrackingType = typeof(BugTrackingSystem);
            MethodInfo method = bugTrackingType.GetMethod(methodName);

            if (method != null)
            {
                object[] bugAttributes = method.GetCustomAttributes(typeof(BugReportAttribute), false);

                if (bugAttributes.Length > 0)
                {
                    Console.WriteLine($"⚠️  Method '{methodName}' has {bugAttributes.Length} known issue(s):\n");

                    foreach (BugReportAttribute bugReport in bugAttributes)
                    {
                        Console.WriteLine($"  ⚠️  {bugReport}");
                    }
                    Console.WriteLine();
                }

                // Execute the method
                BugTrackingSystem system = new BugTrackingSystem();
                Console.WriteLine($"Executing {methodName}...");
                method.Invoke(system, parameters);
            }
            else
            {
                Console.WriteLine($"Method '{methodName}' not found.");
            }
        }
    }
}
