using System;
using AddressBook.Menu;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    ADDRESS BOOK SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("\nFeatures:");
            Console.WriteLine("✓ Create and manage multiple address books");
            Console.WriteLine("✓ Add, edit, delete contacts");
            Console.WriteLine("✓ Search by city/state");
            Console.WriteLine("✓ Sort contacts");
            Console.WriteLine("✓ CSV, JSON, Database, and JSON Server support");
            Console.WriteLine("✓ Multithreaded non-blocking IO operations");
            Console.WriteLine("✓ Validation using reflection and attributes");
            Console.WriteLine("✓ Duplicate prevention");
            Console.WriteLine("\n========================================\n");

            try
            {
                AddressBookMenu menu = new AddressBookMenu();
                menu.ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nThank you for using Address Book System!");
        }
    }
}
