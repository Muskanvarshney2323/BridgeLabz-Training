using AddressBookSystem.Models;

namespace AddressBookSystem.Interfaces
{
    public interface IAddressBook
    {
        void AddContact();
        void AddMultipleContacts();
        void EditContact();
        void DeleteContact();
        void SearchPersonByCityOrState();
        void ViewPersonsByCityOrState();
        void CountPersonsByCityOrState();
        void SortContactsByName();
        void SortContactsByCityStateOrZip();
        void WriteAddressBookToCSV();
        void ReadAddressBookFromCSV();
        void WriteAddressBookToJSON();
        void ReadAddressBookFromJSON();
        void WriteAddressBookToJsonServer();
        void ReadAddressBookFromJsonServer();
        void SaveAddressBookToDatabase();
    }

    public interface IAddressBookSystem
    {
        void AddAddressBook();
        void SwitchAddressBook();
        bool IsAddressBookSelected();
        string GetCurrentAddressBookName();
    }

    public interface IAddressBookRepository
    {
        void AddContact(Person person);
        void DeleteContact(string firstName, string lastName);
        void EditContact(string firstName, string lastName, Person updatedPerson);
        List<Person> GetAllContacts();
        Person? FindByName(string firstName, string lastName);
        List<Person> FindByCityOrState(string cityOrState);
        int CountByCityOrState(string cityOrState);
        bool IsDuplicate(Person person);
        void SortByName();
        void SortByCityStateZip();
    }

    public interface IDataSource
    {
        Task<List<Person>> ReadAsync(string fileName);
        Task WriteAsync(string fileName, List<Person> persons);
    }

    public interface IInputValidator
    {
        bool IsValidEmail(string email);
        bool IsValidPhoneNumber(string phone);
        bool IsValidZip(string zip);
        string GetValidInput(string prompt, Func<string, bool> validator);
    }
}
