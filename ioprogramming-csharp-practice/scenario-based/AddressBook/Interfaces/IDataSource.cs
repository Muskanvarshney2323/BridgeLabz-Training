using System.Collections.Generic;
using AddressBook.Models;

namespace AddressBook.Interfaces
{
    /// <summary>
    /// Interface defining the contract for data sources (CSV, JSON, Database, etc.)
    /// Follows Open/Closed Principle - open for extension, closed for modification
    /// </summary>
    public interface IDataSource
    {
        void Write(List<Contact> contacts, string addressBookName);
        List<Contact> Read(string addressBookName);
        bool IsAvailable();
        string GetSourceName();
    }
}
