using System;
using System.Collections;

namespace CollectionAnnotations.Exercises
{
    /// <summary>
    /// Exercise 3: Suppress Warnings for Unchecked Operations
    /// Create an ArrayList without generics and use #pragma warning 
    /// disables to hide compilation warnings.
    /// </summary>
    class SuppressUncheckedWarnings
    {
        static void Main()
        {
            Console.WriteLine("=== Suppress Warnings for Unchecked Operations ===\n");

            Console.WriteLine("Example 1: Using ArrayList without warnings suppression");
            DemonstrateWithoutSuppression();

            Console.WriteLine("\n\nExample 2: Using ArrayList with suppressed warnings");
            DemonstrateWithSuppression();

            Console.WriteLine("\n\nExample 3: Mixed operations");
            MixedOperations();
        }

        static void DemonstrateWithoutSuppression()
        {
            // This would normally generate a warning about unchecked cast
            ArrayList list = new ArrayList
            {
                "John",
                "Alice",
                "Bob"
            };

            Console.WriteLine("ArrayList contents:");
            foreach (var item in list)
            {
                Console.WriteLine($"  - {item}");
            }
        }

        static void DemonstrateWithSuppression()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            ArrayList list = new ArrayList
            {
                "Apple",
                "Banana",
                "Orange"
            };

            Console.WriteLine("ArrayList contents (without warnings):");
            foreach (var item in list)
            {
                Console.WriteLine($"  - {item}");
            }
#pragma warning restore CS0618

            Console.WriteLine("Warnings suppression disabled after this point.");
        }

        static void MixedOperations()
        {
#pragma warning disable CS0618
            // Create an ArrayList with mixed types
            ArrayList mixedList = new ArrayList();
            mixedList.Add("String value");
            mixedList.Add(42);
            mixedList.Add(3.14);
            mixedList.Add(true);

            Console.WriteLine("Mixed ArrayList contents:");
            for (int i = 0; i < mixedList.Count; i++)
            {
                object item = mixedList[i];
                Console.WriteLine($"  [{i}] {item} (Type: {item.GetType().Name})");
            }

            // Unchecked cast example
            Console.WriteLine("\nUnchecked operations:");
            string str = (string)mixedList[0]; // Safe - string is stored
            int num = (int)mixedList[1];       // Safe - int is stored
            
            Console.WriteLine($"String value: {str}");
            Console.WriteLine($"Integer value: {num}");

            // Suppress specific warning about unsafe cast
#pragma warning disable CS0219
            try
            {
                // This is unsafe - trying to cast int to string
                string wrongCast = (string)mixedList[1]; // This would fail at runtime
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
#pragma warning restore CS0219

#pragma warning restore CS0618
        }

        // Example showing specific warning suppression
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CS0618:Type or member is obsolete")]
        static void SuppressViaAttribute()
        {
            // Method attribute suppresses warnings for the entire method
#pragma warning disable
            ArrayList list = new ArrayList();
            list.Add("Suppressed");
#pragma warning restore
        }
    }
}
