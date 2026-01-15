using System;
using System.Collections.Generic;

class AddressBookSystem
{
    // ===== UC-6 START =====
    // Dictionary to maintain multiple Address Books
    private Dictionary<string, AddressBook> addressBooks =
        new Dictionary<string, AddressBook>();
    // ===== UC-6 END =====

    // UC-6: Create new Address Book
    public void AddAddressBook(string name)
    {
        if (addressBooks.ContainsKey(name))
        {
            Console.WriteLine("Address Book already exists.");
            return;
        }

        addressBooks[name] = new AddressBook();
        Console.WriteLine($"Address Book '{name}' created.");
    }

    // UC-6: Select Address Book
    public AddressBook GetAddressBook(string name)
    {
        if (addressBooks.ContainsKey(name))
        {
            return addressBooks[name];
        }

        Console.WriteLine("Address Book not found.");
        return null;
    }
}
