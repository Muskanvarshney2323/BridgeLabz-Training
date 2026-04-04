using System;

class AddressBookMenu
{
    public void ShowMenu()
    {
        Console.WriteLine("Welcome to Address Book Program");

        AddressBookSystem system = new AddressBookSystem();   // UC-6
        AddressBook addressBook = null;
        IAddressBookUtility utility = new AddressBookUtility();

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n1. Create Address Book");
            Console.WriteLine("2. Select Address Book");
            Console.WriteLine("3. Add Contact");
            Console.WriteLine("4. Edit Contact");
            Console.WriteLine("5. Delete Contact");
            Console.WriteLine("6. Exit");

            Console.Write("Enter choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Address Book Name: ");
                    system.AddAddressBook(Console.ReadLine());
                    break;

                case 2:
                    Console.Write("Enter Address Book Name: ");
                    string name = Console.ReadLine();
                    AddressBook book = system.GetAddressBook(name);

                    if (book != null)
                        book.AddContact(utility.GetContactDetails());
                    else
                        Console.WriteLine("Address Book not found.");
                    break;

                case 3:   // UC8
                    Console.Write("Enter City: ");
                    system.SearchPersonByCity(Console.ReadLine());
                    break;

                case 4:   // UC8
                    Console.Write("Enter State: ");
                    system.SearchPersonByState(Console.ReadLine());
                    break;

                case 5:
                    return;
            }
        }
    }
}
