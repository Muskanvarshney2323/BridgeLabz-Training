using System;
using System.Collections.Generic;

class AddressBook
{
    private List<ContactPerson> contacts = new List<ContactPerson>();

    public void AddContact(ContactPerson person)
    {
        contacts.Add(person);
    }

    public void EditContact(string firstName)
    {
        foreach (ContactPerson person in contacts)
        {
            if (person.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter New Address: ");
                person.Address = Console.ReadLine();

                Console.Write("Enter New City: ");
                person.City = Console.ReadLine();

                Console.Write("Enter New State: ");
                person.State = Console.ReadLine();

                Console.Write("Enter New Zip: ");
                person.Zip = Console.ReadLine();

                Console.Write("Enter New Phone Number: ");
                person.PhoneNumber = Console.ReadLine();

                Console.Write("Enter New Email: ");
                person.Email = Console.ReadLine();

                Console.WriteLine("Contact updated successfully.");
                return;
            }
        }
        Console.WriteLine("Contact not found.");
    }
    public void DeleteContact(string firstName)
    {
        for (int i = 0; i < contacts.Count; i++)
        {
            if (contacts[i].FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
            {
                contacts.RemoveAt(i);
                Console.WriteLine("Contact deleted successfully.");
                return;
            }
        }
        Console.WriteLine("Contact not found.");
    }
}