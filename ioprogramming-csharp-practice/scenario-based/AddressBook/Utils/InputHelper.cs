using System;
using System.Collections.Generic;
using AddressBook.Models;

namespace AddressBook.Utils
{
    /// <summary>
    /// InputHelper for getting contact information from user
    /// Demonstrates user input handling and contact creation
    /// </summary>
    public class InputHelper
    {
        /// <summary>
        /// Prompts user for contact details and creates a new Contact object
        /// </summary>
        public static Contact GetContactFromInput()
        {
            Console.WriteLine("\n--- Enter Contact Details ---");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone Number (10 digits): ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("State: ");
            string state = Console.ReadLine();

            Console.Write("Zip Code (5 digits): ");
            string zip = Console.ReadLine();

            return new Contact(firstName, lastName, email, phoneNumber, address, city, state, zip);
        }

        /// <summary>
        /// Gets multiple contacts from user input
        /// </summary>
        public static List<Contact> GetMultipleContactsFromInput()
        {
            List<Contact> contacts = new List<Contact>();

            Console.Write("How many contacts would you like to add? ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"\n--- Contact {i + 1} ---");
                    Contact contact = GetContactFromInput();
                    contacts.Add(contact);
                }
            }

            return contacts;
        }

        /// <summary>
        /// Gets a name from user for search/edit/delete operations
        /// </summary>
        public static (string FirstName, string LastName) GetNameFromInput()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            return (firstName, lastName);
        }

        /// <summary>
        /// Gets city or state for search/view/count operations
        /// </summary>
        public static (string SearchType, string SearchValue) GetCityOrStateInput()
        {
            Console.WriteLine("Search by:");
            Console.WriteLine("1. City");
            Console.WriteLine("2. State");
            Console.Write("Choose (1/2): ");

            string choice = Console.ReadLine();
            string searchType = choice == "1" ? "City" : "State";

            Console.Write($"Enter {searchType}: ");
            string searchValue = Console.ReadLine();

            return (searchType, searchValue);
        }

        /// <summary>
        /// Gets sort preference for contacts
        /// </summary>
        public static string GetSortPreference()
        {
            Console.WriteLine("\nSort by:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. City");
            Console.WriteLine("3. State");
            Console.WriteLine("4. Zip");
            Console.Write("Choose (1-4): ");

            return Console.ReadLine();
        }
    }
}
