using AddressBookSystem.Exceptions;
using AddressBookSystem.Interfaces;
using AddressBookSystem.Models;
using AddressBookSystem.Utilities;

namespace AddressBookSystem.Services
{
    public class AddressBookUtilityImpl : IAddressBook, IAddressBookSystem
    {
        private Dictionary<string, AddressBookRepository> addressBooks;
        private AddressBookRepository? currentAddressBook;
        private string? currentAddressBookName;
        private InputValidator validator;
        private DatabaseHelper databaseHelper;

        public AddressBookUtilityImpl()
        {
            addressBooks = new Dictionary<string, AddressBookRepository>();
            validator = new InputValidator();
            databaseHelper = new DatabaseHelper();
        }

        // IAddressBookSystem Implementation
        public void AddAddressBook()
        {
            Console.Write("Enter Address Book name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Address Book name cannot be empty");
                return;
            }

            if (addressBooks.ContainsKey(name))
            {
                Console.WriteLine("Address Book with this name already exists");
                return;
            }

            addressBooks[name] = new AddressBookRepository();
            Console.WriteLine($"Address Book '{name}' created successfully");
        }

        public void SwitchAddressBook()
        {
            if (addressBooks.Count == 0)
            {
                Console.WriteLine("No Address Books available. Create one first.");
                return;
            }

            Console.WriteLine("\nAvailable Address Books:");
            int index = 1;
            foreach (var key in addressBooks.Keys)
            {
                Console.WriteLine($"{index}. {key}");
                index++;
            }

            Console.Write("Select Address Book (enter number): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= addressBooks.Count)
            {
                currentAddressBookName = addressBooks.Keys.ElementAt(choice - 1);
                currentAddressBook = addressBooks[currentAddressBookName];
                Console.WriteLine($"Switched to Address Book: {currentAddressBookName}");
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }

        public bool IsAddressBookSelected()
        {
            return currentAddressBook != null;
        }

        public string GetCurrentAddressBookName()
        {
            return currentAddressBookName ?? "None";
        }

        // IAddressBook Implementation
        public void AddContact()
        {
            if (!IsAddressBookSelected() || currentAddressBook == null)
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Person person = GetPersonFromInput();
                currentAddressBook.AddContact(person);
            }
            catch (DuplicateContactException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void AddMultipleContacts()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            Console.Write("How many contacts you want to add? ");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"\n--- Adding Contact {i + 1} ---");
                    try
                    {
                        Person person = GetPersonFromInput();
                        currentAddressBook.AddContact(person);
                    }
                    catch (DuplicateContactException ex)
                    {
                        Console.WriteLine($"Skipping: {ex.Message}");
                    }
                    catch (InvalidInputException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

        public void EditContact()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter first name to edit: ");
                string? firstName = Console.ReadLine();
                Console.Write("Enter last name: ");
                string? lastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Name cannot be empty");
                    return;
                }

                Console.WriteLine("\nEnter new details (leave blank to keep current):");
                Person updatedPerson = GetPersonFromInput(firstName, lastName);
                currentAddressBook.EditContact(firstName, lastName, updatedPerson);
            }
            catch (ContactNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteContact()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter first name to delete: ");
                string? firstName = Console.ReadLine();
                Console.Write("Enter last name: ");
                string? lastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Name cannot be empty");
                    return;
                }

                currentAddressBook.DeleteContact(firstName, lastName);
            }
            catch (ContactNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void SearchPersonByCityOrState()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            Console.Write("Enter city or state to search: ");
            string? cityOrState = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cityOrState))
            {
                Console.WriteLine("City or State cannot be empty");
                return;
            }

            List<Person> results = currentAddressBook.FindByCityOrState(cityOrState);

            if (results.Count == 0)
            {
                Console.WriteLine($"No contacts found for city/state: {cityOrState}");
            }
            else
            {
                Console.WriteLine($"\nContacts in {cityOrState}:");
                foreach (var person in results)
                {
                    Console.WriteLine(person.ToString());
                }
            }
        }

        public void ViewPersonsByCityOrState()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            Console.Write("Enter city or state to view: ");
            string? cityOrState = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cityOrState))
            {
                Console.WriteLine("City or State cannot be empty");
                return;
            }

            List<Person> results = currentAddressBook.FindByCityOrState(cityOrState);

            if (results.Count == 0)
            {
                Console.WriteLine($"No contacts found for city/state: {cityOrState}");
            }
            else
            {
                Console.WriteLine($"\n=== Persons in {cityOrState} ===");
                foreach (var person in results)
                {
                    Console.WriteLine(person.ToString());
                }
            }
        }

        public void CountPersonsByCityOrState()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            Console.Write("Enter city or state to count: ");
            string? cityOrState = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cityOrState))
            {
                Console.WriteLine("City or State cannot be empty");
                return;
            }

            int count = currentAddressBook.CountByCityOrState(cityOrState);
            Console.WriteLine($"Number of contacts in {cityOrState}: {count}");
        }

        public void SortContactsByName()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            currentAddressBook.SortByName();
            Console.WriteLine("\n=== Contacts sorted by name ===");
            currentAddressBook.DisplayAllContacts();
        }

        public void SortContactsByCityStateOrZip()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            currentAddressBook.SortByCityStateZip();
            Console.WriteLine("\n=== Contacts sorted by city, state, zip ===");
            currentAddressBook.DisplayAllContacts();
        }

        public void WriteAddressBookToCSV()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter file name (with .csv extension): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("File name cannot be empty");
                    return;
                }

                string filePath = Path.Combine("Data", fileName);
                List<Person> contacts = currentAddressBook.GetAllContacts();

                Task task = FileOperationHelper.WriteCSVAsync(filePath, contacts);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ReadAddressBookFromCSV()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter file name (with .csv extension): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("File name cannot be empty");
                    return;
                }

                string filePath = Path.Combine("Data", fileName);
                Task<List<Person>> task = FileOperationHelper.ReadCSVAsync(filePath);
                task.Wait();
                List<Person> persons = task.Result;

                foreach (var person in persons)
                {
                    try
                    {
                        currentAddressBook.AddContact(person);
                    }
                    catch (DuplicateContactException)
                    {
                        // Skip duplicates
                    }
                }

                Console.WriteLine($"Loaded {persons.Count} contacts from CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void WriteAddressBookToJSON()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter file name (with .json extension): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("File name cannot be empty");
                    return;
                }

                string filePath = Path.Combine("Data", fileName);
                List<Person> contacts = currentAddressBook.GetAllContacts();

                Task task = FileOperationHelper.WriteJSONAsync(filePath, contacts);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ReadAddressBookFromJSON()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter file name (with .json extension): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("File name cannot be empty");
                    return;
                }

                string filePath = Path.Combine("Data", fileName);
                Task<List<Person>> task = FileOperationHelper.ReadJSONAsync(filePath);
                task.Wait();
                List<Person> persons = task.Result;

                foreach (var person in persons)
                {
                    try
                    {
                        currentAddressBook.AddContact(person);
                    }
                    catch (DuplicateContactException)
                    {
                        // Skip duplicates
                    }
                }

                Console.WriteLine($"Loaded {persons.Count} contacts from JSON");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void WriteAddressBookToJsonServer()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter JSON Server URL (e.g., http://localhost:3000/contacts): ");
                string? serverUrl = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(serverUrl))
                {
                    Console.WriteLine("Server URL cannot be empty");
                    return;
                }

                List<Person> contacts = currentAddressBook.GetAllContacts();
                Task task = FileOperationHelper.WriteToJsonServerAsync(serverUrl, contacts);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ReadAddressBookFromJsonServer()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                Console.Write("Enter JSON Server URL (e.g., http://localhost:3000/contacts): ");
                string? serverUrl = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(serverUrl))
                {
                    Console.WriteLine("Server URL cannot be empty");
                    return;
                }

                Task<List<Person>> task = FileOperationHelper.ReadFromJsonServerAsync(serverUrl);
                task.Wait();
                List<Person> persons = task.Result;

                foreach (var person in persons)
                {
                    try
                    {
                        currentAddressBook.AddContact(person);
                    }
                    catch (DuplicateContactException)
                    {
                        // Skip duplicates
                    }
                }

                Console.WriteLine($"Loaded {persons.Count} contacts from JSON Server");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void SaveAddressBookToDatabase()
        {
            if (!IsAddressBookSelected())
            {
                Console.WriteLine("Please select an Address Book first");
                return;
            }

            try
            {
                List<Person> contacts = currentAddressBook.GetAllContacts();
                Task task = databaseHelper.WriteToDatabaseAsync(currentAddressBookName ?? "default", contacts);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Helper method to get person input from user
        private Person GetPersonFromInput(string? existingFirstName = null, string? existingLastName = null)
        {
            Person person = new Person();

            if (string.IsNullOrWhiteSpace(existingFirstName))
            {
                Console.Write("First Name: ");
                person.FirstName = Console.ReadLine() ?? "";
            }
            else
            {
                person.FirstName = existingFirstName;
            }

            if (string.IsNullOrWhiteSpace(existingLastName))
            {
                Console.Write("Last Name: ");
                person.LastName = Console.ReadLine() ?? "";
            }
            else
            {
                person.LastName = existingLastName;
            }

            Console.Write("Address: ");
            person.Address = Console.ReadLine() ?? "";

            Console.Write("City: ");
            person.City = Console.ReadLine() ?? "";

            Console.Write("State: ");
            person.State = Console.ReadLine() ?? "";

            Console.Write("Zip: ");
            person.Zip = Console.ReadLine() ?? "";

            Console.Write("Phone Number: ");
            person.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            person.Email = Console.ReadLine() ?? "";

            return person;
        }

        public List<Person> GetAllContacts()
        {
            return currentAddressBook?.GetAllContacts() ?? new List<Person>();
        }
    }
}
