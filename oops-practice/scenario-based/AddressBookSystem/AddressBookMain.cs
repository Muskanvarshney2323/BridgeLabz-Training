using System;
class AddressBookMain
{
    public void Start()
    {
        Console.WriteLine("Welcome to Address Book Program");

        IAddressBookUtility utility = new AddressBookUtility();
        IAddressBook addressBook = new AddressBook();
        bool exit = false;
        while(!exit)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Contact Person");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case 1:
                    ContactPerson person = utility.GetContactDetails();
                    addressBook.AddContactPerson(person);
                    utility.ShowMessage("Contact Person Added Successfully");
                    break;
                case 2:
                    exit = true;
                    utility.ShowMessage("Exiting Address Book Program.");
                    break;
                default:
                    utility.ShowMessage("Invalid choice. Please try again.");
                    break;
            }
        }
        
    }
}
