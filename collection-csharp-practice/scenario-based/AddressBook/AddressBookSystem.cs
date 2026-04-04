using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// UC 5, UC 8: AddressBookSystem class manages multiple Address Books
/// UC 5: Refactor to add multiple Address Book to the System
/// Each Address Book has a unique Name
/// UC 8: Ability to search Person in a City or State across the multiple Address Books
/// </summary>
public class AddressBookSystem
{
    // Dictionary to maintain AddressBook Name to AddressBook mapping (UC 5)
    private Dictionary<string, AddressBook> addressBooks;

    /// <summary>
    /// Constructor to initialize AddressBookSystem
    /// </summary>
    public AddressBookSystem()
    {
        addressBooks = new Dictionary<string, AddressBook>();
    }

    /// <summary>
    /// UC 5: Add a new Address Book to the System
    /// Each Address Book has a unique Name
    /// </summary>
    public void AddAddressBook(string name)
    {
        if (addressBooks.ContainsKey(name))
        {
            Console.WriteLine($"Error: Address Book '{name}' already exists!");
            return;
        }

        AddressBook newAddressBook = new AddressBook(name);
        addressBooks[name] = newAddressBook;
        Console.WriteLine($"Address Book '{name}' created successfully!");
    }

    /// <summary>
    /// Get a specific Address Book by name
    /// </summary>
    public AddressBook GetAddressBook(string name)
    {
        if (!addressBooks.ContainsKey(name))
        {
            Console.WriteLine($"Error: Address Book '{name}' not found!");
            return null;
        }

        return addressBooks[name];
    }

    /// <summary>
    /// UC 8: Search Person in a City or State across the multiple Address Books
    /// Search Result can show multiple person in the city or state
    /// </summary>
    public void SearchPersonAcrossAddressBooks(string searchTerm, bool isCity)
    {
        List<Contact> results = new List<Contact>();
        string searchType = isCity ? "City" : "State";

        Console.WriteLine($"\n=== Searching across all Address Books by {searchType}: {searchTerm} ===\n");

        bool found = false;

        foreach (var addressBook in addressBooks.Values)
        {
            // Get contacts by city or state from each address book
            var contacts = isCity 
                ? addressBook.GetContactsByCity(searchTerm) 
                : addressBook.GetContactsByState(searchTerm);
            
            if (contacts.Count > 0)
            {
                found = true;
                Console.WriteLine($"\nIn Address Book '{addressBook.Name}':");
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"  {contact.FirstName} {contact.LastName} - {searchType}: {searchTerm}");
                }
                Console.WriteLine(new string('-', 50));
            }
        }

        if (!found)
        {
            Console.WriteLine($"No persons found in {searchType} '{searchTerm}' across all Address Books!");
        }
    }

    /// <summary>
    /// Display all Address Books
    /// </summary>
    public void DisplayAllAddressBooks()
    {
        if (addressBooks.Count == 0)
        {
            Console.WriteLine("No Address Books created yet!");
            return;
        }

        Console.WriteLine("\n=== All Address Books ===\n");
        foreach (var name in addressBooks.Keys)
        {
            int count = addressBooks[name].GetTotalContacts();
            Console.WriteLine($"- {name} ({count} contacts)");
        }
    }

    /// <summary>
    /// Delete an Address Book by name
    /// </summary>
    public void DeleteAddressBook(string name)
    {
        if (addressBooks.Remove(name))
        {
            Console.WriteLine($"Address Book '{name}' deleted successfully!");
        }
        else
        {
            Console.WriteLine($"Error: Address Book '{name}' not found!");
        }
    }

    /// <summary>
    /// Get total number of Address Books
    /// </summary>
    public int GetTotalAddressBooks()
    {
        return addressBooks.Count;
    }
}
