using System;
using AddressBook.Models;

namespace AddressBook.Events
{
    /// <summary>
    /// EventArgs for contact operations
    /// Used with delegates to notify about contact changes
    /// </summary>
    public class ContactEventArgs : EventArgs
    {
        public Contact Contact { get; set; }
        public string Operation { get; set; } // "Added", "Updated", "Deleted"
        public DateTime Timestamp { get; set; }
        public string AddressBookName { get; set; }

        public ContactEventArgs(Contact contact, string operation, string addressBookName)
        {
            Contact = contact;
            Operation = operation;
            Timestamp = DateTime.Now;
            AddressBookName = addressBookName;
        }
    }

    /// <summary>
    /// Delegate for contact change notifications
    /// </summary>
    public delegate void ContactEventHandler(object sender, ContactEventArgs e);

    /// <summary>
    /// EventArgs for address book operations
    /// </summary>
    public class AddressBookEventArgs : EventArgs
    {
        public string AddressBookName { get; set; }
        public string Operation { get; set; } // "Created", "Switched", "Deleted"
        public DateTime Timestamp { get; set; }

        public AddressBookEventArgs(string addressBookName, string operation)
        {
            AddressBookName = addressBookName;
            Operation = operation;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Delegate for address book change notifications
    /// </summary>
    public delegate void AddressBookEventHandler(object sender, AddressBookEventArgs e);
}
