using System;

class AddressBook : IAddressBook
{
    // Storage for contacts
    private ContactPerson[] contacts;
    private int count;

    // Constructor
    public AddressBook()
    {
        contacts = new ContactPerson[10]; // fixed size for UC-1
        count = 0;
    }

    // Business logic: Add contact
    public void AddContactPerson(ContactPerson person)
    {
        if (count >= contacts.Length)
        {
            Console.WriteLine("Address Book is full. Cannot add more contacts.");
            return;
        }

        contacts[count] = person;
        count++;

        Console.WriteLine("Contact Person Added Successfully");
    }
}
