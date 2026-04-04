using System;

class AddressBook
{
    private ContactPerson[] contacts;
    private int count;

    public AddressBook()
    {
        contacts = new ContactPerson[100]; // fixed size array
        count = 0;
    }

    // UC-1, UC-2, UC-5 : Add Contact
    public void AddContact(ContactPerson person)
    {
       // ================= UC7 =================
        // Duplicate check using array + Equals()
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].Equals(person))
            {
                Console.WriteLine("Duplicate contact found. Person already exists.");
                return;
            }
        }

        contacts[count] = person;
        count++;
    }
    // ================= UC8 =================
    // Search person by City
    public void SearchByCity(string city)
    {
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].City.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                PrintContact(contacts[i]);
            }
        }
    }

    // Search person by State
    public void SearchByState(string state)
    {
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].State.Equals(state, StringComparison.OrdinalIgnoreCase))
            {
                PrintContact(contacts[i]);
            }
        }
    }

    private void PrintContact(ContactPerson p)
    {
        Console.WriteLine($"{p.FirstName} {p.LastName} | {p.City} | {p.State} | {p.PhoneNumber}");
    }

    // UC-3 : Edit Contact
    public void EditContact(string firstName)
    {
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter New Address: ");
                contacts[i].Address = Console.ReadLine();

                Console.Write("Enter New City: ");
                contacts[i].City = Console.ReadLine();

                Console.Write("Enter New State: ");
                contacts[i].State = Console.ReadLine();

                Console.Write("Enter New Zip: ");
                contacts[i].Zip = Console.ReadLine();

                Console.Write("Enter New Phone Number: ");
                contacts[i].PhoneNumber = Console.ReadLine();

                Console.Write("Enter New Email: ");
                contacts[i].Email = Console.ReadLine();

                Console.WriteLine("Contact updated successfully.");
                return;
            }
        }

        Console.WriteLine("Contact not found.");
    }

    // UC-4 : Delete Contact
    public void DeleteContact(string firstName)
    {
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
            {
                // shift elements
                for (int j = i; j < count - 1; j++)
                {
                    contacts[j] = contacts[j + 1];
                }

                contacts[count - 1] = null;
                count--;

                Console.WriteLine("Contact deleted successfully.");
                return;
            }
        }

        Console.WriteLine("Contact not found.");
    }
}
