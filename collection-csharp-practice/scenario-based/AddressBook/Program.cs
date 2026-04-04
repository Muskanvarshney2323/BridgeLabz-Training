using System;

/// <summary>
/// Program.cs: Application Entry Point
/// 
/// This is the main entry point for the Address Book Application
/// It delegates to the AddressBookMain class which manages the entire application flow
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Call AddressBookMain to start the application
        AddressBookMain.Main(args);
    }
}
