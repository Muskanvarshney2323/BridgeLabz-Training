using System;
using AddressBook.Interfaces;
using AddressBook.Services;

namespace AddressBook.Menu
{
    public class AddressBookMenu
    {
        private IAddressBookSystem system;
        private IAddressBook contacts;

        public AddressBookMenu()
        {
            AddressBookUtilityImpl impl = new AddressBookUtilityImpl();
            system = impl;
            contacts = impl;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== ADDRESS BOOK SYSTEM ===");
                Console.WriteLine("1. Create Address Book");
                Console.WriteLine("2. Switch Address Book");
                Console.WriteLine("0. Exit");

                int ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 0: return;
                    case 1: system.AddAddressBook(); break;
                    case 2: system.SwitchAddressBook(); break;
                    default: Console.WriteLine("Invalid choice"); break;
                }

                if (!system.IsAddressBookSelected())
                    continue;

                while (true)
                {
                    Console.WriteLine($"\n--- Address Book: {system.GetCurrentAddressBookName()} ---");
                    Console.WriteLine("1. Add Contact");
                    Console.WriteLine("2. Add Multiple Contacts");
                    Console.WriteLine("3. Edit Contact");
                    Console.WriteLine("4. Delete Contact");
                    Console.WriteLine("5. Search by City/State");
                    Console.WriteLine("6. View by City/State");
                    Console.WriteLine("7. Count by City/State");
                    Console.WriteLine("8. Sort by Name");
                    Console.WriteLine("9. Sort by City/State/Zip");
                    Console.WriteLine("10. Save to CSV");
                    Console.WriteLine("11. Load from CSV");
                    Console.WriteLine("12. Save to JSON File");
                    Console.WriteLine("13. Load from JSON File");
                    Console.WriteLine("14. Save to JSON Server");
                    Console.WriteLine("15. Load from JSON Server");
                    Console.WriteLine("16. Save to Database");
                    Console.WriteLine("0. Back");

                    int op = Convert.ToInt32(Console.ReadLine());

                    switch (op)
                    {
                        case 0: break;
                        case 1: contacts.AddContact(); break;
                        case 2: contacts.AddMultipleContacts(); break;
                        case 3: contacts.EditContact(); break;
                        case 4: contacts.DeleteContact(); break;
                        case 5: contacts.SearchPersonByCityOrState(); break;
                        case 6: contacts.ViewPersonsByCityOrState(); break;
                        case 7: contacts.CountPersonsByCityOrState(); break;
                        case 8: contacts.SortContactsByName(); break;
                        case 9: contacts.SortContactsByCityStateOrZip(); break;
                        case 10: contacts.WriteAddressBookToCSV(); break;
                        case 11: contacts.ReadAddressBookFromCSV(); break;
                        case 12: contacts.WriteAddressBookToJSON(); break;
                        case 13: contacts.ReadAddressBookFromJSON(); break;
                        case 14: contacts.WriteAddressBookToJsonServer(); break;
                        case 15: contacts.ReadAddressBookFromJsonServer(); break;
                        case 16: contacts.SaveAddressBookToDatabase(); break;
                        default: Console.WriteLine("Invalid option"); break;
                    }

                    if (op == 0) break;
                }
            }
        }
    }
}
