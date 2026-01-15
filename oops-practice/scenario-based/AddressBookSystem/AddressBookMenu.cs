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
                // ===== UC-6 START =====
                case 1:
                    Console.Write("Enter Address Book Name: ");
                    system.AddAddressBook(Console.ReadLine());
                    break;

                case 2:
                    Console.Write("Enter Address Book Name: ");
                    addressBook = system.GetAddressBook(Console.ReadLine());
                    break;
                // ===== UC-6 END =====

                case 3:
                    if (addressBook == null)
                    {
                        Console.WriteLine("Select Address Book first.");
                        break;
                    }
                    ContactPerson person = utility.GetContactDetails();
                    addressBook.AddContact(person);
                    break;

                case 4:
                    Console.Write("Enter First Name: ");
                    addressBook.EditContact(Console.ReadLine());
                    break;

                case 5:
                    Console.Write("Enter First Name: ");
                    addressBook.DeleteContact(Console.ReadLine());
                    break;

                case 6:
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
            }
        }
    }
}
