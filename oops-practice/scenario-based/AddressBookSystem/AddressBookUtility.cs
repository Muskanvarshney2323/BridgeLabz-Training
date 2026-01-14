using System;

class AddressBookUtility : IAddressBookUtility
{
    public ContactPerson GetContactDetails()
    {
        ContactPerson person = new ContactPerson();

        Console.Write("Enter First Name: ");
        person.FirstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        person.LastName = Console.ReadLine();

        Console.Write("Enter Address: ");
        person.Address = Console.ReadLine();

        Console.Write("Enter City: ");
        person.City = Console.ReadLine();

        Console.Write("Enter State: ");
        person.State = Console.ReadLine();

        Console.Write("Enter Zip Code: ");
        person.Zip = Console.ReadLine();

        Console.Write("Enter Phone Number: ");
        person.PhoneNumber = Console.ReadLine();

        Console.Write("Enter Email ID: ");
        person.Email = Console.ReadLine();

        return person;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
