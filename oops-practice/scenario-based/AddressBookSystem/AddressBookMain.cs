using System;

class AddressBookMain
{
    private AddressBookMenu menu;

    public AddressBookMain()
    {
        // Initialize the menu class
        menu = new AddressBookMenu();
    }

    // Start method to control the program flow
    public void Start()
    {
        menu.ShowMenu();
    }
}
