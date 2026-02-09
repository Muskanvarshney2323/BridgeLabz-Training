using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AddressBook.Attributes;
using AddressBook.DataSources;
using AddressBook.Events;
using AddressBook.Interfaces;
using AddressBook.Models;
using AddressBook.Utils;

namespace AddressBook.Services
{
    /// <summary>
    /// Main service implementation for Address Book System
    /// Implements IAddressBook and IAddressBookSystem interfaces
    /// Demonstrates generics, collections, delegates, events, and multithreading
    /// UC1-18: All use cases implemented
    /// </summary>
    public class AddressBookUtilityImpl : IAddressBook, IAddressBookSystem
    {
        // Collections to store address books and data sources
        private Dictionary<string, List<Contact>> _addressBooks;
        private string _currentAddressBook;
        private List<IDataSource> _dataSources;

        // Events
        public event ContactEventHandler ContactChanged;
        public event AddressBookEventHandler AddressBookChanged;

        public AddressBookUtilityImpl()
        {
            _addressBooks = new Dictionary<string, List<Contact>>();
            _currentAddressBook = null;
            _dataSources = new List<IDataSource>
            {
                new CsvDataSource("./Data"),
                new JsonFileDataSource("./Data"),
                new JsonServerDataSource("http://localhost:3000"),
                new DatabaseDataSource()
            };
        }

        #region UC17 - Multiple Address Books

        /// <summary>
        /// UC17: Create a new Address Book with unique name
        /// Refactored to support multiple address books
        /// </summary>
        public void AddAddressBook()
        {
            Console.Write("Enter Address Book name: ");
            string bookName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bookName))
            {
                Console.WriteLine("Address Book name cannot be empty");
                return;
            }

            if (_addressBooks.ContainsKey(bookName))
            {
                Console.WriteLine("Address Book with this name already exists");
                return;
            }

            _addressBooks[bookName] = new List<Contact>();
            _currentAddressBook = bookName;

            RaiseAddressBookChanged(bookName, "Created");
            Console.WriteLine($"Address Book '{bookName}' created successfully");
        }

        /// <summary>
        /// UC17: Switch between existing Address Books
        /// </summary>
        public void SwitchAddressBook()
        {
            if (_addressBooks.Count == 0)
            {
                Console.WriteLine("No Address Books available. Create one first.");
                return;
            }

            Console.WriteLine("\nAvailable Address Books:");
            int index = 1;
            foreach (var key in _addressBooks.Keys)
            {
                Console.WriteLine($"{index}. {key} ({_addressBooks[key].Count} contacts)");
                index++;
            }

            Console.Write("Choose an Address Book: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _addressBooks.Count)
            {
                string selectedBook = _addressBooks.Keys.ElementAt(choice - 1);
                _currentAddressBook = selectedBook;
                RaiseAddressBookChanged(selectedBook, "Switched");
                Console.WriteLine($"Switched to Address Book: {selectedBook}");
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }

        public string GetCurrentAddressBookName()
        {
            return _currentAddressBook ?? "No Address Book Selected";
        }

        public bool IsAddressBookSelected()
        {
            return !string.IsNullOrEmpty(_currentAddressBook) && _addressBooks.ContainsKey(_currentAddressBook);
        }

        #endregion

        #region UC1 - Add Single Contact

        /// <summary>
        /// UC1: Add a new Contact with validation
        /// Uses reflection-based validation via attributes
        /// </summary>
        public void AddContact()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            Contact contact = InputHelper.GetContactFromInput();

            // UC18: Validate using reflection and attributes
            if (!ValidationHelper.ValidateContact(contact))
            {
                Console.WriteLine("Contact validation failed");
                return;
            }

            // UC18: Check for duplicate (prevent duplicate entries in same address book)
            if (_addressBooks[_currentAddressBook].Any(c => c.Equals(contact)))
            {
                Console.WriteLine("Duplicate contact: This person already exists in this Address Book");
                return;
            }

            _addressBooks[_currentAddressBook].Add(contact);
            RaiseContactChanged(contact, "Added");
            Console.WriteLine("Contact added successfully");
        }

        #endregion

        #region UC2 - Add Multiple Contacts

        /// <summary>
        /// UC2: Add multiple contacts at once
        /// Demonstrates collections and batch operations
        /// </summary>
        public void AddMultipleContacts()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            List<Contact> newContacts = InputHelper.GetMultipleContactsFromInput();

            if (newContacts.Count == 0)
            {
                Console.WriteLine("No contacts to add");
                return;
            }

            int addedCount = 0;
            foreach (Contact contact in newContacts)
            {
                if (!ValidationHelper.ValidateContact(contact))
                {
                    Console.WriteLine($"Skipped invalid contact: {contact.FirstName}");
                    continue;
                }

                if (_addressBooks[_currentAddressBook].Any(c => c.Equals(contact)))
                {
                    Console.WriteLine($"Skipped duplicate: {contact.FirstName} {contact.LastName}");
                    continue;
                }

                _addressBooks[_currentAddressBook].Add(contact);
                RaiseContactChanged(contact, "Added");
                addedCount++;
            }

            Console.WriteLine($"Successfully added {addedCount} contacts");
        }

        #endregion

        #region UC3 - Edit Contact

        /// <summary>
        /// UC3: Edit existing contact by name
        /// </summary>
        public void EditContact()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            (string firstName, string lastName) = InputHelper.GetNameFromInput();

            Contact existingContact = _addressBooks[_currentAddressBook]
                .FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                    c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (existingContact == null)
            {
                Console.WriteLine("Contact not found");
                return;
            }

            Console.WriteLine("\nCurrent Contact Details:");
            Console.WriteLine(existingContact);

            Console.WriteLine("\nEnter new values (press Enter to keep current value):");

            Console.Write($"Email [{existingContact.Email}]: ");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
                existingContact.Email = email;

            Console.Write($"Phone Number [{existingContact.PhoneNumber}]: ");
            string phone = Console.ReadLine();
            if (!string.IsNullOrEmpty(phone))
                existingContact.PhoneNumber = phone;

            Console.Write($"Address [{existingContact.Address}]: ");
            string address = Console.ReadLine();
            if (!string.IsNullOrEmpty(address))
                existingContact.Address = address;

            Console.Write($"City [{existingContact.City}]: ");
            string city = Console.ReadLine();
            if (!string.IsNullOrEmpty(city))
                existingContact.City = city;

            Console.Write($"State [{existingContact.State}]: ");
            string state = Console.ReadLine();
            if (!string.IsNullOrEmpty(state))
                existingContact.State = state;

            Console.Write($"Zip [{existingContact.Zip}]: ");
            string zip = Console.ReadLine();
            if (!string.IsNullOrEmpty(zip))
                existingContact.Zip = zip;

            if (ValidationHelper.ValidateContact(existingContact))
            {
                RaiseContactChanged(existingContact, "Updated");
                Console.WriteLine("Contact updated successfully");
            }
            else
            {
                Console.WriteLine("Updated contact failed validation");
            }
        }

        #endregion

        #region UC4 - Delete Contact

        /// <summary>
        /// UC4: Delete a contact by name
        /// </summary>
        public void DeleteContact()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            (string firstName, string lastName) = InputHelper.GetNameFromInput();

            Contact contactToDelete = _addressBooks[_currentAddressBook]
                .FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                    c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contactToDelete == null)
            {
                Console.WriteLine("Contact not found");
                return;
            }

            _addressBooks[_currentAddressBook].Remove(contactToDelete);
            RaiseContactChanged(contactToDelete, "Deleted");
            Console.WriteLine("Contact deleted successfully");
        }

        #endregion

        #region UC5 - Search by City/State Across Multiple Address Books

        /// <summary>
        /// UC5: Search for persons by City or State across multiple Address Books
        /// Demonstrates iteration through multiple collections
        /// </summary>
        public void SearchPersonByCityOrState()
        {
            if (_addressBooks.Count == 0)
            {
                Console.WriteLine("No Address Books available");
                return;
            }

            (string searchType, string searchValue) = InputHelper.GetCityOrStateInput();

            List<Contact> results = new List<Contact>();
            foreach (var addressBook in _addressBooks.Values)
            {
                foreach (Contact contact in addressBook)
                {
                    if ((searchType == "City" && contact.City.Equals(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                        (searchType == "State" && contact.State.Equals(searchValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        results.Add(contact);
                    }
                }
            }

            if (results.Count == 0)
            {
                Console.WriteLine($"No persons found in {searchValue}");
            }
            else
            {
                Console.WriteLine($"\nPersons found in {searchValue}:");
                DisplayContacts(results);
            }
        }

        #endregion

        #region UC6 - View by City/State

        /// <summary>
        /// UC6: View persons by City or State in current Address Book
        /// </summary>
        public void ViewPersonsByCityOrState()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            (string searchType, string searchValue) = InputHelper.GetCityOrStateInput();

            var results = _addressBooks[_currentAddressBook]
                .FindAll(c => (searchType == "City" && c.City.Equals(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                            (searchType == "State" && c.State.Equals(searchValue, StringComparison.OrdinalIgnoreCase)));

            if (results.Count == 0)
            {
                Console.WriteLine($"No persons found for {searchValue}");
            }
            else
            {
                Console.WriteLine($"\nPersons in {searchValue}:");
                DisplayContacts(results);
            }
        }

        #endregion

        #region UC7 - Count by City/State

        /// <summary>
        /// UC7: Get count of persons by City or State
        /// </summary>
        public void CountPersonsByCityOrState()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            (string searchType, string searchValue) = InputHelper.GetCityOrStateInput();

            int count = 0;
            foreach (Contact contact in _addressBooks[_currentAddressBook])
            {
                if ((searchType == "City" && contact.City.Equals(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                    (searchType == "State" && contact.State.Equals(searchValue, StringComparison.OrdinalIgnoreCase)))
                {
                    count++;
                }
            }

            Console.WriteLine($"Total persons in {searchValue}: {count}");
        }

        #endregion

        #region UC8 - Sort by Name

        /// <summary>
        /// UC8: Sort contacts alphabetically by Person's name
        /// Demonstrates generic collections and sorting
        /// </summary>
        public void SortContactsByName()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            if (_addressBooks[_currentAddressBook].Count == 0)
            {
                Console.WriteLine("No contacts to sort");
                return;
            }

            List<Contact> sortedList = new List<Contact>(_addressBooks[_currentAddressBook]);
            
            // Manual bubble sort to avoid LINQ
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = 0; j < sortedList.Count - i - 1; j++)
                {
                    string name1 = sortedList[j].FirstName + " " + sortedList[j].LastName;
                    string name2 = sortedList[j + 1].FirstName + " " + sortedList[j + 1].LastName;

                    if (string.Compare(name1, name2) > 0)
                    {
                        Contact temp = sortedList[j];
                        sortedList[j] = sortedList[j + 1];
                        sortedList[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nContacts sorted by Name:");
            DisplayContacts(sortedList);
        }

        #endregion

        #region UC9 - Sort by City/State/Zip

        /// <summary>
        /// UC9: Sort contacts by City, State, or Zip
        /// </summary>
        public void SortContactsByCityStateOrZip()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            if (_addressBooks[_currentAddressBook].Count == 0)
            {
                Console.WriteLine("No contacts to sort");
                return;
            }

            string sortChoice = InputHelper.GetSortPreference();
            List<Contact> sortedList = new List<Contact>(_addressBooks[_currentAddressBook]);

            // Manual sort based on choice (no LINQ as per requirements)
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = 0; j < sortedList.Count - i - 1; j++)
                {
                    bool shouldSwap = false;

                    switch (sortChoice)
                    {
                        case "2": // City
                            shouldSwap = string.Compare(sortedList[j].City, sortedList[j + 1].City) > 0;
                            break;
                        case "3": // State
                            shouldSwap = string.Compare(sortedList[j].State, sortedList[j + 1].State) > 0;
                            break;
                        case "4": // Zip
                            shouldSwap = string.Compare(sortedList[j].Zip, sortedList[j + 1].Zip) > 0;
                            break;
                        default:
                            shouldSwap = string.Compare(sortedList[j].FirstName, sortedList[j + 1].FirstName) > 0;
                            break;
                    }

                    if (shouldSwap)
                    {
                        Contact temp = sortedList[j];
                        sortedList[j] = sortedList[j + 1];
                        sortedList[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nContacts sorted:");
            DisplayContacts(sortedList);
        }

        #endregion

        #region UC10-11 - CSV Operations

        /// <summary>
        /// UC10: Write Address Book to CSV File
        /// UC17: Ensures non-blocking IO using async operations
        /// </summary>
        public void WriteAddressBookToCSV()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            List<Contact> contacts = _addressBooks[_currentAddressBook];

            // UC17: Non-blocking async operation
            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource csvSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "CSV File");
                    if (csvSource != null)
                    {
                        csvSource.Write(contacts, _currentAddressBook);
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            Console.WriteLine("CSV save operation started...");
        }

        /// <summary>
        /// UC11: Load Address Book from CSV File
        /// </summary>
        public void ReadAddressBookFromCSV()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource csvSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "CSV File");
                    if (csvSource != null)
                    {
                        List<Contact> loadedContacts = csvSource.Read(_currentAddressBook);
                        _addressBooks[_currentAddressBook] = loadedContacts;
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            AsyncHelper.WaitForAsyncOperation();
            Console.WriteLine("CSV load completed");
        }

        #endregion

        #region UC12-13 - JSON File Operations

        /// <summary>
        /// UC12: Write Address Book to JSON File
        /// </summary>
        public void WriteAddressBookToJSON()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            List<Contact> contacts = _addressBooks[_currentAddressBook];

            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource jsonSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "JSON File");
                    if (jsonSource != null)
                    {
                        jsonSource.Write(contacts, _currentAddressBook);
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            Console.WriteLine("JSON save operation started...");
        }

        /// <summary>
        /// UC13: Load Address Book from JSON File
        /// </summary>
        public void ReadAddressBookFromJSON()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource jsonSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "JSON File");
                    if (jsonSource != null)
                    {
                        List<Contact> loadedContacts = jsonSource.Read(_currentAddressBook);
                        _addressBooks[_currentAddressBook] = loadedContacts;
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            AsyncHelper.WaitForAsyncOperation();
            Console.WriteLine("JSON load completed");
        }

        #endregion

        #region UC14-15 - JSON Server Operations

        /// <summary>
        /// UC14: Write Address Book to JSON Server
        /// </summary>
        public void WriteAddressBookToJsonServer()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            List<Contact> contacts = _addressBooks[_currentAddressBook];

            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource jsonServerSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "JSON Server");
                    if (jsonServerSource != null)
                    {
                        jsonServerSource.Write(contacts, _currentAddressBook);
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            Console.WriteLine("JSON Server save operation started...");
        }

        /// <summary>
        /// UC15: Load Address Book from JSON Server
        /// </summary>
        public void ReadAddressBookFromJsonServer()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource jsonServerSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "JSON Server");
                    if (jsonServerSource != null)
                    {
                        List<Contact> loadedContacts = jsonServerSource.Read(_currentAddressBook);
                        if (loadedContacts.Count > 0)
                        {
                            _addressBooks[_currentAddressBook] = loadedContacts;
                        }
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            AsyncHelper.WaitForAsyncOperation();
            Console.WriteLine("JSON Server load completed");
        }

        #endregion

        #region UC16 - Database Operations

        /// <summary>
        /// UC16: Save Address Book to Database
        /// Follows Open/Closed Principle - extensible for multiple database providers
        /// </summary>
        public void SaveAddressBookToDatabase()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            List<Contact> contacts = _addressBooks[_currentAddressBook];

            AsyncHelper.ExecuteAsync(
                () =>
                {
                    IDataSource dbSource = _dataSources.FirstOrDefault(d => d.GetSourceName() == "SQL Database");
                    if (dbSource != null)
                    {
                        if (!dbSource.IsAvailable())
                        {
                            Console.WriteLine("Note: Database is not available.");
                            Console.WriteLine("To use database feature, ensure SQL Server is running.");
                            Console.WriteLine("Create a database named 'AddressBookDB' or update the connection string.");
                            return;
                        }

                        dbSource.Write(contacts, _currentAddressBook);
                    }
                },
                (success, message) =>
                {
                    Console.WriteLine(message);
                });

            Console.WriteLine("Database save operation started...");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get all contacts in current address book
        /// </summary>
        public List<Contact> GetAllContacts()
        {
            if (!IsAddressBookSelected())
                return new List<Contact>();

            return new List<Contact>(_addressBooks[_currentAddressBook]);
        }

        /// <summary>
        /// Display contacts in tabular format
        /// </summary>
        public void DisplayContacts(List<Contact> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts to display");
                return;
            }

            Console.WriteLine("\n{0,-15} {1,-15} {2,-25} {3,-15} {4,-20} {5,-15} {6,-10} {7,-10}",
                "First Name", "Last Name", "Email", "Phone", "City", "State", "Zip", "Address");
            Console.WriteLine(new string('-', 140));

            foreach (Contact contact in contacts)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-20} {5,-15} {6,-10} {7,-10}",
                    contact.FirstName ?? "", contact.LastName ?? "", contact.Email ?? "",
                    contact.PhoneNumber ?? "", contact.City ?? "", contact.State ?? "",
                    contact.Zip ?? "", contact.Address ?? "");
            }
        }

        /// <summary>
        /// Check if contact exists by name
        /// </summary>
        public bool ContactExists(string firstName, string lastName)
        {
            if (!IsAddressBookSelected())
                return false;

            return _addressBooks[_currentAddressBook]
                .Any(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                         c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Event Helpers

        private void RaiseContactChanged(Contact contact, string operation)
        {
            ContactChanged?.Invoke(this, new ContactEventArgs(contact, operation, _currentAddressBook));
        }

        private void RaiseAddressBookChanged(string addressBookName, string operation)
        {
            AddressBookChanged?.Invoke(this, new AddressBookEventArgs(addressBookName, operation));
        }

        #endregion
    }
}
