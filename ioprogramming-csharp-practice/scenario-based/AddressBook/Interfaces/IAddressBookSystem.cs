using AddressBook.Events;

namespace AddressBook.Interfaces
{
    /// <summary>
    /// Interface for system-level Address Book operations
    /// Manages multiple address books and the overall system
    /// UC17: Refactored to support multiple address books
    /// </summary>
    public interface IAddressBookSystem
    {
        // UC17: Multiple address books with unique names
        void AddAddressBook();
        void SwitchAddressBook();

        string GetCurrentAddressBookName();
        bool IsAddressBookSelected();

        // Events for system-level notifications
        event AddressBookEventHandler AddressBookChanged;
    }
}
