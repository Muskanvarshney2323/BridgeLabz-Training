using System.Collections.Generic;
using AddressBook.Models;
using AddressBook.Events;

namespace AddressBook.Interfaces
{
    /// <summary>
    /// Interface for individual Address Book operations
    /// Includes CRUD operations and search functionality
    /// </summary>
    public interface IAddressBook
    {
        // UC1: Add a new contact
        void AddContact();

        // UC2: Add multiple contacts
        void AddMultipleContacts();

        // UC3: Edit existing contact
        void EditContact();

        // UC4: Delete contact
        void DeleteContact();

        // UC5: Search person by city or state across multiple address books
        void SearchPersonByCityOrState();

        // UC6: View persons by city or state
        void ViewPersonsByCityOrState();

        // UC7: Count persons by city or state
        void CountPersonsByCityOrState();

        // UC8: Sort contacts by name
        void SortContactsByName();

        // UC9: Sort contacts by city, state, or zip
        void SortContactsByCityStateOrZip();

        // UC10-13: File I/O operations
        void WriteAddressBookToCSV();
        void ReadAddressBookFromCSV();
        void WriteAddressBookToJSON();
        void ReadAddressBookFromJSON();

        // UC14-15: JSON Server operations
        void WriteAddressBookToJsonServer();
        void ReadAddressBookFromJsonServer();

        // UC16: Database operations
        void SaveAddressBookToDatabase();

        // Internal helper methods
        List<Contact> GetAllContacts();
        void DisplayContacts(List<Contact> contacts);
        bool ContactExists(string firstName, string lastName);

        // Events
        event ContactEventHandler ContactChanged;
    }
}
