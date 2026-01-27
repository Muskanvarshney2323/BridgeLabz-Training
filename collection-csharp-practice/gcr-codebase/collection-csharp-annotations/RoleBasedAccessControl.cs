using System;
using System.Collections.Generic;
using System.Reflection;

namespace CollectionAnnotations.Advanced
{
    /// <summary>
    /// Advanced Level - Problem 5: Implement Role-Based Access Control with RoleAllowed
    /// Define a class-level attribute RoleAllowed to restrict method access based on roles.
    /// </summary>

    // Custom RoleAllowed Attribute
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleAllowedAttribute : Attribute
    {
        public string[] AllowedRoles { get; set; }

        public RoleAllowedAttribute(params string[] roles)
        {
            AllowedRoles = roles;
        }
    }

    // User context to track current user's role
    public static class UserContext
    {
        public static string CurrentUserRole { get; set; } = "USER";
        public static string CurrentUsername { get; set; } = "Guest";
    }

    class AdminPanel
    {
        [RoleAllowed("ADMIN")]
        public void DeleteUser(string userId)
        {
            Console.WriteLine($"Deleting user: {userId}");
        }

        [RoleAllowed("ADMIN", "MODERATOR")]
        public void BanUser(string userId)
        {
            Console.WriteLine($"Banning user: {userId}");
        }

        [RoleAllowed("ADMIN")]
        public void ViewSystemLogs()
        {
            Console.WriteLine("Displaying system logs...");
        }

        [RoleAllowed("ADMIN", "MODERATOR", "USER")]
        public void ViewProfile(string userId)
        {
            Console.WriteLine($"Viewing profile: {userId}");
        }

        public void PublicMethod()
        {
            Console.WriteLine("This is a public method - no role restriction.");
        }
    }

    class BankingSystem
    {
        [RoleAllowed("ADMIN")]
        public void TransferMoneyBetweenAccounts(string from, string to, double amount)
        {
            Console.WriteLine($"Transferring ${amount} from {from} to {to}");
        }

        [RoleAllowed("EMPLOYEE")]
        public void ProcessLoan(string customerId)
        {
            Console.WriteLine($"Processing loan for: {customerId}");
        }

        [RoleAllowed("ADMIN", "EMPLOYEE", "CUSTOMER")]
        public void ViewBalance(string accountId)
        {
            Console.WriteLine($"Balance for account {accountId}: $5000");
        }

        public void CheckInterestRates()
        {
            Console.WriteLine("Current interest rates: 3.5% - 5.2%");
        }
    }

    class RoleBasedAccessControlDemo
    {
        static void Main()
        {
            Console.WriteLine("=== Role-Based Access Control - Advanced Level ===\n");

            // Example 1: Admin access
            Console.WriteLine("Example 1: Admin User Access\n");
            PerformWithRoleCheck("ADMIN", "Alice Admin");

            // Example 2: User access
            Console.WriteLine("\n\nExample 2: Regular User Access\n");
            PerformWithRoleCheck("USER", "Bob User");

            // Example 3: Moderator access
            Console.WriteLine("\n\nExample 3: Moderator Access\n");
            PerformWithRoleCheck("MODERATOR", "Carol Moderator");

            // Example 4: Banking system with employee role
            Console.WriteLine("\n\nExample 4: Banking System - Employee Access\n");
            BankingDemo("EMPLOYEE", "David Employee");

            // Example 5: Display access control matrix
            Console.WriteLine("\n\nExample 5: Access Control Matrix\n");
            DisplayAccessControlMatrix();
        }

        static void PerformWithRoleCheck(string role, string username)
        {
            UserContext.CurrentUserRole = role;
            UserContext.CurrentUsername = username;

            AdminPanel adminPanel = new AdminPanel();
            Type adminType = typeof(AdminPanel);

            MethodInfo[] methods = adminType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            Console.WriteLine($"User: {username} (Role: {role})\n");

            foreach (MethodInfo method in methods)
            {
                var roleAttr = method.GetCustomAttribute<RoleAllowedAttribute>();

                if (roleAttr != null)
                {
                    bool hasAccess = IsAuthorized(roleAttr.AllowedRoles);

                    if (hasAccess)
                    {
                        Console.WriteLine($"✓ {method.Name} - Access Granted");
                        InvokeMethod(adminPanel, method);
                    }
                    else
                    {
                        Console.WriteLine($"✗ {method.Name} - Access Denied!");
                    }
                }
                else
                {
                    Console.WriteLine($"○ {method.Name} - No restriction");
                    InvokeMethod(adminPanel, method);
                }
                Console.WriteLine();
            }
        }

        static void BankingDemo(string role, string username)
        {
            UserContext.CurrentUserRole = role;
            UserContext.CurrentUsername = username;

            BankingSystem bankingSystem = new BankingSystem();
            Type bankingType = typeof(BankingSystem);

            MethodInfo[] methods = bankingType.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.DeclaredOnly);

            Console.WriteLine($"User: {username} (Role: {role})\n");

            foreach (MethodInfo method in methods)
            {
                var roleAttr = method.GetCustomAttribute<RoleAllowedAttribute>();

                if (roleAttr != null)
                {
                    bool hasAccess = IsAuthorized(roleAttr.AllowedRoles);

                    if (hasAccess)
                    {
                        Console.WriteLine($"✓ {method.Name} - Access Granted");
                        InvokeMethodWithParams(bankingSystem, method);
                    }
                    else
                    {
                        Console.WriteLine($"✗ {method.Name} - Access Denied!");
                    }
                }
                else
                {
                    Console.WriteLine($"○ {method.Name} - No restriction");
                    InvokeMethodWithParams(bankingSystem, method);
                }
                Console.WriteLine();
            }
        }

        static void DisplayAccessControlMatrix()
        {
            Type[] types = { typeof(AdminPanel), typeof(BankingSystem) };

            foreach (Type type in types)
            {
                Console.WriteLine($"\n{type.Name}:");
                Console.WriteLine("{0,-30} {1,-40}", "Method", "Allowed Roles");
                Console.WriteLine(new string('-', 70));

                MethodInfo[] methods = type.GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.DeclaredOnly);

                foreach (MethodInfo method in methods)
                {
                    var roleAttr = method.GetCustomAttribute<RoleAllowedAttribute>();

                    if (roleAttr != null)
                    {
                        string roles = string.Join(", ", roleAttr.AllowedRoles);
                        Console.WriteLine("{0,-30} {1,-40}", method.Name, roles);
                    }
                    else
                    {
                        Console.WriteLine("{0,-30} {1,-40}", method.Name, "Public");
                    }
                }
            }
        }

        static bool IsAuthorized(string[] allowedRoles)
        {
            foreach (string role in allowedRoles)
            {
                if (UserContext.CurrentUserRole == role)
                {
                    return true;
                }
            }
            return false;
        }

        static void InvokeMethod(object instance, MethodInfo method)
        {
            try
            {
                object[] parameters = GetDefaultParameters(method);
                method.Invoke(instance, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
            }
        }

        static void InvokeMethodWithParams(object instance, MethodInfo method)
        {
            try
            {
                object[] parameters = GetDefaultParameters(method);
                method.Invoke(instance, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
            }
        }

        static object[] GetDefaultParameters(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] values = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(string))
                    values[i] = "USER123";
                else if (parameters[i].ParameterType == typeof(double))
                    values[i] = 1000.0;
            }

            return values;
        }
    }
}
