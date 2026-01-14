using System;
class AddressBookMain
{
    public void Start()
    {
        Console.WriteLine("Welcome to Address Book Program");

        IAddressBookUtility utility = new AddressBookUtility();
        ContactPerson person = utility.GetContactDetails();

        IAddressBook addressBook = new AddressBook();
        addressBook.AddContactPerson(person);
    }
}
