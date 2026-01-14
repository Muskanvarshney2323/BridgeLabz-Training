using System;

class AddressBookMenu
{
    private IAddressBookUtility utility;
    private AddressBook addressBook;

    public AddressBookMenu()
    {
        utility = new AddressBookUtility();
        addressBook = new AddressBook();
    }

    public void ShowMenu()   // ✅ menu method, NOT Main
    {
        while (true)
        {
            Console.WriteLine("\nWelcome to Address Book Program");
            Console.WriteLine("\n----- MENU -----");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Edit Contact");
            Console.WriteLine("3. Delete Contact");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ContactPerson person = utility.GetContactDetails();
                    addressBook.AddContact(person);
                    utility.ShowMessage("Contact added successfully.");
                    break;

                case 2:
                    Console.Write("Enter First Name to edit: ");
                    string editName = Console.ReadLine();
                    addressBook.EditContact(editName);
                    break;

                case 3:
                    Console.Write("Enter First Name to delete: ");
                    string deleteName = Console.ReadLine();
                    addressBook.DeleteContact(deleteName);
                    break;

                case 4:
                    Console.WriteLine("Exiting Address Book Program");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
