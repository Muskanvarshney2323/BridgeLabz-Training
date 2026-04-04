using System;

namespace CollectionAnnotations.Exercises
{
    /// <summary>
    /// Exercise 2: Use Obsolete Attribute to Mark an Old Method
    /// Create a class LegacyAPI with an old method OldFeature(), 
    /// which should not be used anymore. Introduce a new method NewFeature().
    /// </summary>
    class ObsoleteAttributeUsage
    {
        static void Main()
        {
            Console.WriteLine("=== Obsolete Attribute Usage ===\n");

            LegacyAPI api = new LegacyAPI();

            // Display warning message
            Console.WriteLine("Calling methods from LegacyAPI:\n");

            // Call the new feature
            Console.WriteLine("--- New Feature (Recommended) ---");
            api.NewFeature();

            Console.WriteLine("\n--- Old Feature (Deprecated) ---");
            // The compiler will show a warning for this call
            api.OldFeature();

            // Display both methods' behavior
            Console.WriteLine("\n\n--- Behavior Comparison ---");
            Console.WriteLine("Old Feature Output:");
            api.OldFeature();

            Console.WriteLine("\nNew Feature Output:");
            api.NewFeature();

            // Show method information
            Console.WriteLine("\n\n--- Method Details ---");
            DisplayMethodInfo();
        }

        static void DisplayMethodInfo()
        {
            Type legacyType = typeof(LegacyAPI);
            System.Reflection.MethodInfo[] methods = legacyType.GetMethods(
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Instance | 
                System.Reflection.BindingFlags.DeclaredOnly);

            foreach (System.Reflection.MethodInfo method in methods)
            {
                Console.WriteLine($"Method: {method.Name}");
                
                // Check if method is obsolete
                var obsoleteAttr = method.GetCustomAttributes(typeof(ObsoleteAttribute), false);
                if (obsoleteAttr.Length > 0)
                {
                    ObsoleteAttribute attr = (ObsoleteAttribute)obsoleteAttr[0];
                    Console.WriteLine($"  Status: OBSOLETE");
                    Console.WriteLine($"  Message: {attr.Message}");
                    Console.WriteLine($"  Throws Error: {attr.IsError}");
                }
                else
                {
                    Console.WriteLine($"  Status: ACTIVE");
                }
                Console.WriteLine();
            }
        }
    }

    class LegacyAPI
    {
        // Mark this method as obsolete with a message
        [Obsolete("OldFeature is deprecated. Use NewFeature instead.", false)]
        public void OldFeature()
        {
            Console.WriteLine("This is the old feature - DEPRECATED!");
            Console.WriteLine("Consider using NewFeature() for better functionality.");
        }

        // New replacement method
        public void NewFeature()
        {
            Console.WriteLine("This is the new feature - RECOMMENDED!");
            Console.WriteLine("Provides better performance and more features.");
        }

        // Example of obsolete method that throws error
        [Obsolete("LegacyMethod is no longer supported. Use ModernMethod instead.", true)]
        public void LegacyMethod()
        {
            Console.WriteLine("This method will cause a compilation error!");
        }

        // Modern replacement
        public void ModernMethod()
        {
            Console.WriteLine("Modern implementation with enhanced features.");
        }

        // Another method with obsolete message
        [Obsolete("UseDeprecatedAPI is outdated. Switch to UseModernAPI for better results.", false)]
        public string UseDeprecatedAPI()
        {
            return "Deprecated API Result";
        }

        public string UseModernAPI()
        {
            return "Modern API Result with enhanced features";
        }
    }
}
