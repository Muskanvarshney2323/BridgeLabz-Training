using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// UC 1-4, UC 6, UC 9-12: AddressBook class manages a single address book
/// Uses List collection to maintain multiple contact persons
/// Maintains Dictionary of City and Person, State and Person
/// </summary>
public class AddressBook
{
    // Name of the Address Book (UC 5)
    public string Name { get; set; }

    // List to store contacts
    private List<Contact> contacts;

    // Dictionary to search persons by City (UC 8, UC 9)
    private Dictionary<string, List<Contact>> cityCon;

    // Dictionary to search persons by State (UC 8, UC 9)
    private Dictionary<string, List<Contact>> stateCon;

    /// <summary>
    /// Constructor to initialize AddressBook with a name
    /// </summary>
    public AddressBook(string name)
    {
        Name = name;
        contacts = new List<Contact>();
        cityCon = new Dictionary<string, List<Contact>>();
        stateCon = new Dictionary<string, List<Contact>>();
    }

    /// <summary>
    /// UC 1: Add a new contact to Address Book
    /// UC 6: Duplicate check is done on Person Name while adding person to Address Book
    /// </summary>
    public void AddContact(Contact contact)
    {
        // UC 6: Check for duplicate entry using Equals override
        if (contacts.Contains(contact))
        {
            Console.WriteLine($"Error: Contact '{contact.FirstName} {contact.LastName}' already exists in {Name}!");
            return;
        }

        contacts.Add(contact);

        // UC 9: Maintain Dictionary of City and Person
        if (!cityCon.ContainsKey(contact.City))
            cityCon[contact.City] = new List<Contact>();
        cityCon[contact.City].Add(contact);

        // UC 9: Maintain Dictionary of State and Person
        if (!stateCon.ContainsKey(contact.State))
            stateCon[contact.State] = new List<Contact>();
        stateCon[contact.State].Add(contact);

        Console.WriteLine($"Contact '{contact.FirstName} {contact.LastName}' added successfully to {Name}!");
    }

    /// <summary>
    /// UC 2: Edit existing contact person using their name
    /// </summary>
    public void EditContact(string firstName, string lastName)
    {
        Contact contact = contacts.FirstOrDefault(c =>
            c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
            c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

        if (contact == null)
        {
            Console.WriteLine($"Error: Contact '{firstName} {lastName}' not found!");
            return;
        }

        Console.WriteLine($"Editing contact: {firstName} {lastName}");
        Console.WriteLine("Enter new details (leave blank to keep current value):");

        Console.Write("Address: ");
        string address = Console.ReadLine();
        if (!string.IsNullOrEmpty(address)) contact.Address = address;

        Console.Write("City: ");
        string city = Console.ReadLine();
        if (!string.IsNullOrEmpty(city))
        {
            // Update dictionary
            cityCon[contact.City].Remove(contact);
            contact.City = city;
            if (!cityCon.ContainsKey(city))
                cityCon[city] = new List<Contact>();
            cityCon[city].Add(contact);
        }

        Console.Write("State: ");
        string state = Console.ReadLine();
        if (!string.IsNullOrEmpty(state))
        {
            // Update dictionary
            stateCon[contact.State].Remove(contact);
            contact.State = state;
            if (!stateCon.ContainsKey(state))
                stateCon[state] = new List<Contact>();
            stateCon[state].Add(contact);
        }

        Console.Write("Zip: ");
        string zip = Console.ReadLine();
        if (!string.IsNullOrEmpty(zip)) contact.Zip = zip;

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();
        if (!string.IsNullOrEmpty(phoneNumber)) contact.PhoneNumber = phoneNumber;

        Console.Write("Email: ");
        string email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email)) contact.Email = email;

        Console.WriteLine($"Contact '{firstName} {lastName}' updated successfully!");
    }

    /// <summary>
    /// UC 3: Delete a person using person's name
    /// </summary>
    public void DeleteContact(string firstName, string lastName)
    {
        Contact contact = contacts.FirstOrDefault(c =>
            c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
            c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

        if (contact == null)
        {
            Console.WriteLine($"Error: Contact '{firstName} {lastName}' not found!");
            return;
        }

        contacts.Remove(contact);

        // Remove from City dictionary
        if (cityCon.ContainsKey(contact.City))
            cityCon[contact.City].Remove(contact);

        // Remove from State dictionary
        if (stateCon.ContainsKey(contact.State))
            stateCon[contact.State].Remove(contact);

        Console.WriteLine($"Contact '{firstName} {lastName}' deleted successfully!");
    }

    /// <summary>
    /// UC 8: Search person in the Address Book by city or state
    /// </summary>
    public void SearchByCityOrState(string searchTerm, bool isCity)
    {
        List<Contact> results = new List<Contact>();

        if (isCity)
        {
            if (cityCon.ContainsKey(searchTerm))
                results = cityCon[searchTerm];
            Console.WriteLine($"\nPersons in City: {searchTerm}");
        }
        else
        {
            if (stateCon.ContainsKey(searchTerm))
                results = stateCon[searchTerm];
            Console.WriteLine($"\nPersons in State: {searchTerm}");
        }

        if (results.Count == 0)
        {
            Console.WriteLine("No persons found!");
            return;
        }

        foreach (var contact in results)
        {
            Console.WriteLine($"\n{contact}");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// UC 9: View Persons by City or State
    /// </summary>
    public void ViewByCity(string city)
    {
        SearchByCityOrState(city, true);
    }

    public void ViewByState(string state)
    {
        SearchByCityOrState(state, false);
    }

    /// <summary>
    /// UC 10: Get number of contact persons by City or State
    /// </summary>
    public void GetCountByCity(string city)
    {
        int count = cityCon.ContainsKey(city) ? cityCon[city].Count : 0;
        Console.WriteLine($"Count of contacts in City '{city}': {count}");
    }

    public void GetCountByState(string state)
    {
        int count = stateCon.ContainsKey(state) ? stateCon[state].Count : 0;
        Console.WriteLine($"Count of contacts in State '{state}': {count}");
    }

    /// <summary>
    /// UC 11: Sort the entries in the address book alphabetically by Person's name
    /// </summary>
    public void SortByName()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts to sort!");
            return;
        }

        List<Contact> sortedContacts = new List<Contact>(contacts);
        sortedContacts.Sort(); // Uses CompareTo method

        Console.WriteLine($"\n=== Address Book '{Name}' Sorted by Name ===\n");
        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact}\n");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// UC 12: Sort the entries in the address book by City
    /// </summary>
    public void SortByCity()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts to sort!");
            return;
        }

        List<Contact> sortedContacts = new List<Contact>(contacts);
        sortedContacts.Sort((c1, c2) => c1.CompareByCity(c2));

        Console.WriteLine($"\n=== Address Book '{Name}' Sorted by City ===\n");
        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact}\n");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// UC 12: Sort the entries in the address book by State
    /// </summary>
    public void SortByState()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts to sort!");
            return;
        }

        List<Contact> sortedContacts = new List<Contact>(contacts);
        sortedContacts.Sort((c1, c2) => c1.CompareByState(c2));

        Console.WriteLine($"\n=== Address Book '{Name}' Sorted by State ===\n");
        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact}\n");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// UC 12: Sort the entries in the address book by Zip
    /// </summary>
    public void SortByZip()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts to sort!");
            return;
        }

        List<Contact> sortedContacts = new List<Contact>(contacts);
        sortedContacts.Sort((c1, c2) => c1.CompareByZip(c2));

        Console.WriteLine($"\n=== Address Book '{Name}' Sorted by Zip ===\n");
        foreach (var contact in sortedContacts)
        {
            Console.WriteLine($"{contact}\n");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// Display all contacts in the address book
    /// </summary>
    public void DisplayAllContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine($"No contacts in {Name}!");
            return;
        }

        Console.WriteLine($"\n=== All Contacts in Address Book '{Name}' ===\n");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact}\n");
            Console.WriteLine(new string('-', 50));
        }
    }

    /// <summary>
    /// Get total number of contacts in the address book
    /// </summary>
    public int GetTotalContacts()
    {
        return contacts.Count;
    }

    /// <summary>
    /// UC 8: Get contacts by city from this address book
    /// Used for searching across multiple address books
    /// </summary>
    public List<Contact> GetContactsByCity(string city)
    {
        if (cityCon.ContainsKey(city))
            return new List<Contact>(cityCon[city]);
        return new List<Contact>();
    }

    /// <summary>
    /// UC 8: Get contacts by state from this address book
    /// Used for searching across multiple address books
    /// </summary>
    public List<Contact> GetContactsByState(string state)
    {
        if (stateCon.ContainsKey(state))
            return new List<Contact>(stateCon[state]);
        return new List<Contact>();
    }
}
