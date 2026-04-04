using System;

/// <summary>
/// AddressBookMain: Main entry point for Address Book Application
/// 
/// Start Master Branch
/// Ability to create Contacts in Address Book with first and last names, address,
/// city, state, zip, phone number and email address
/// 
/// Program is written using IDE like Visual Studio
/// Every UC is in a separate Git Branch and then merged with main
/// Naming Convention, Indentation, etc Code Hygiene will be checked during Review
/// Git Check In Comments and Version History will be monitored
/// 
/// Supported Use Cases (UC):
/// UC 1: Add a new Contact to Address Book
/// UC 2: Edit existing contact person using their name
/// UC 3: Delete a person using person's name
/// UC 4: Add multiple person to Address Book (using Collection Class)
/// UC 5: Multiple Address Books with unique Name (using Dictionary)
/// UC 6: Ensure no Duplicate Entry of the same Person
/// UC 7: Override equals method to check for Duplicate
/// UC 8: Search Person in a City or State across multiple Address Books
/// UC 9: View Persons by City or State (using Dictionary)
/// UC 10: Get number of contact persons by City or State
/// UC 11: Sort entries alphabetically by Person's name
/// UC 12: Sort entries by City, State, or Zip
/// </summary>
public class AddressBookMain
{
    // Global instance of AddressBookSystem
    private static AddressBookSystem system;

    static void Main(string[] args)
    {
        // Initialize the Address Book System
        system = new AddressBookSystem();

        // Display Welcome Message
        DisplayWelcome();

        // Main Menu Loop
        bool isRunning = true;
        while (isRunning)
        {
            DisplayMainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageAddressBooks();
                    break;
                case "2":
                    ManageContacts();
                    break;
                case "3":
                    SearchAndView();
                    break;
                case "4":
                    Console.WriteLine("Exiting Address Book Application. Goodbye!");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Display Welcome Message
    /// </summary>
    static void DisplayWelcome()
    {
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════╗");
        Console.WriteLine("║   Welcome to Address Book Program         ║");
        Console.WriteLine("║                                           ║");
        Console.WriteLine("║   Version 1.0 - Master Branch             ║");
        Console.WriteLine("╚═══════════════════════════════════════════╝\n");
    }

    /// <summary>
    /// Display Main Menu
    /// </summary>
    static void DisplayMainMenu()
    {
        Console.WriteLine("\n═══════════════════════════════════════════");
        Console.WriteLine("           Main Menu");
        Console.WriteLine("═══════════════════════════════════════════");
        Console.WriteLine("1. Manage Address Books (UC 5)");
        Console.WriteLine("2. Manage Contacts (UC 1-4, UC 6-7)");
        Console.WriteLine("3. Search & View Contacts (UC 8-12)");
        Console.WriteLine("4. Exit");
        Console.WriteLine("═══════════════════════════════════════════");
        Console.Write("Enter your choice: ");
    }

    /// <summary>
    /// UC 5: Manage Address Books
    /// </summary>
    static void ManageAddressBooks()
    {
        bool managing = true;
        while (managing)
        {
            Console.WriteLine("\n───────────────────────────────────────────");
            Console.WriteLine("       Address Book Management (UC 5)");
            Console.WriteLine("───────────────────────────────────────────");
            Console.WriteLine("1. Create New Address Book");
            Console.WriteLine("2. View All Address Books");
            Console.WriteLine("3. Delete Address Book");
            Console.WriteLine("4. Back to Main Menu");
            Console.WriteLine("───────────────────────────────────────────");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAddressBook();
                    break;
                case "2":
                    system.DisplayAllAddressBooks();
                    break;
                case "3":
                    DeleteAddressBook();
                    break;
                case "4":
                    managing = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    /// <summary>
    /// Create a new Address Book
    /// </summary>
    static void CreateAddressBook()
    {
        Console.Write("Enter Address Book Name: ");
        string name = Console.ReadLine();
        system.AddAddressBook(name);
    }

    /// <summary>
    /// Delete an Address Book
    /// </summary>
    static void DeleteAddressBook()
    {
        Console.Write("Enter Address Book Name to Delete: ");
        string name = Console.ReadLine();
        system.DeleteAddressBook(name);
    }

    /// <summary>
    /// UC 1-4, UC 6-7: Manage Contacts in an Address Book
    /// </summary>
    static void ManageContacts()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();

        AddressBook addressBook = system.GetAddressBook(bookName);
        if (addressBook == null)
            return;

        bool managing = true;
        while (managing)
        {
            Console.WriteLine("\n───────────────────────────────────────────");
            Console.WriteLine($"    Contact Management: {bookName}");
            Console.WriteLine("───────────────────────────────────────────");
            Console.WriteLine("1. Add New Contact (UC 1)");
            Console.WriteLine("2. Edit Contact (UC 2)");
            Console.WriteLine("3. Delete Contact (UC 3)");
            Console.WriteLine("4. Display All Contacts (UC 4)");
            Console.WriteLine("5. Back to Main Menu");
            Console.WriteLine("───────────────────────────────────────────");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddContact(addressBook);
                    break;
                case "2":
                    EditContact(addressBook);
                    break;
                case "3":
                    DeleteContact(addressBook);
                    break;
                case "4":
                    addressBook.DisplayAllContacts();
                    break;
                case "5":
                    managing = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    /// <summary>
    /// UC 1: Add a new Contact to Address Book
    /// </summary>
    static void AddContact(AddressBook addressBook)
    {
        Console.WriteLine("\n--- Add New Contact (UC 1) ---");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Address: ");
        string address = Console.ReadLine();

        Console.Write("City: ");
        string city = Console.ReadLine();

        Console.Write("State: ");
        string state = Console.ReadLine();

        Console.Write("Zip: ");
        string zip = Console.ReadLine();

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Contact contact = new Contact(firstName, lastName, address, city, state, zip, phoneNumber, email);
        addressBook.AddContact(contact);
    }

    /// <summary>
    /// UC 2: Edit existing contact person using their name
    /// </summary>
    static void EditContact(AddressBook addressBook)
    {
        Console.WriteLine("\n--- Edit Contact (UC 2) ---");
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        addressBook.EditContact(firstName, lastName);
    }

    /// <summary>
    /// UC 3: Delete a person using person's name
    /// </summary>
    static void DeleteContact(AddressBook addressBook)
    {
        Console.WriteLine("\n--- Delete Contact (UC 3) ---");
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        addressBook.DeleteContact(firstName, lastName);
    }

    /// <summary>
    /// UC 8-12: Search and View Contacts
    /// </summary>
    static void SearchAndView()
    {
        bool searching = true;
        while (searching)
        {
            Console.WriteLine("\n───────────────────────────────────────────");
            Console.WriteLine("  Search & View Contacts (UC 8-12)");
            Console.WriteLine("───────────────────────────────────────────");
            Console.WriteLine("1. Search by City or State (UC 8)");
            Console.WriteLine("2. View by City (UC 9)");
            Console.WriteLine("3. View by State (UC 9)");
            Console.WriteLine("4. Get Count by City (UC 10)");
            Console.WriteLine("5. Get Count by State (UC 10)");
            Console.WriteLine("6. Sort by Name (UC 11)");
            Console.WriteLine("7. Sort by City (UC 12)");
            Console.WriteLine("8. Sort by State (UC 12)");
            Console.WriteLine("9. Sort by Zip (UC 12)");
            Console.WriteLine("10. Back to Main Menu");
            Console.WriteLine("───────────────────────────────────────────");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SearchAcrossBooks();
                    break;
                case "2":
                    ViewByCity();
                    break;
                case "3":
                    ViewByState();
                    break;
                case "4":
                    CountByCity();
                    break;
                case "5":
                    CountByState();
                    break;
                case "6":
                    SortByName();
                    break;
                case "7":
                    SortByCity();
                    break;
                case "8":
                    SortByState();
                    break;
                case "9":
                    SortByZip();
                    break;
                case "10":
                    searching = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    /// <summary>
    /// UC 8: Search Person across multiple Address Books
    /// </summary>
    static void SearchAcrossBooks()
    {
        Console.WriteLine("\n--- Search Across Address Books (UC 8) ---");
        Console.WriteLine("1. Search by City");
        Console.WriteLine("2. Search by State");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();
        bool isCity = choice == "1";

        if (choice == "1" || choice == "2")
        {
            Console.Write(choice == "1" ? "Enter City: " : "Enter State: ");
            string searchTerm = Console.ReadLine();
            system.SearchPersonAcrossAddressBooks(searchTerm, isCity);
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
    }

    /// <summary>
    /// UC 9: View Persons by City
    /// </summary>
    static void ViewByCity()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            addressBook.ViewByCity(city);
        }
    }

    /// <summary>
    /// UC 9: View Persons by State
    /// </summary>
    static void ViewByState()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            Console.Write("Enter State: ");
            string state = Console.ReadLine();
            addressBook.ViewByState(state);
        }
    }

    /// <summary>
    /// UC 10: Get Count by City
    /// </summary>
    static void CountByCity()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            addressBook.GetCountByCity(city);
        }
    }

    /// <summary>
    /// UC 10: Get Count by State
    /// </summary>
    static void CountByState()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            Console.Write("Enter State: ");
            string state = Console.ReadLine();
            addressBook.GetCountByState(state);
        }
    }

    /// <summary>
    /// UC 11: Sort by Name
    /// </summary>
    static void SortByName()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            addressBook.SortByName();
        }
    }

    /// <summary>
    /// UC 12: Sort by City
    /// </summary>
    static void SortByCity()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            addressBook.SortByCity();
        }
    }

    /// <summary>
    /// UC 12: Sort by State
    /// </summary>
    static void SortByState()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            addressBook.SortByState();
        }
    }

    /// <summary>
    /// UC 12: Sort by Zip
    /// </summary>
    static void SortByZip()
    {
        Console.Write("Enter Address Book Name: ");
        string bookName = Console.ReadLine();
        AddressBook addressBook = system.GetAddressBook(bookName);

        if (addressBook != null)
        {
            addressBook.SortByZip();
        }
    }
}
