using AddressBookSystem.Exceptions;
using AddressBookSystem.Interfaces;
using AddressBookSystem.Models;
using AddressBookSystem.Utilities;

namespace AddressBookSystem.Services
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private List<Person> contacts;

        public AddressBookRepository()
        {
            contacts = new List<Person>();
        }

        public void AddContact(Person person)
        {
            if (person == null)
                throw new InvalidInputException("Person cannot be null");

            if (IsDuplicate(person))
                throw new DuplicateContactException($"Contact '{person.FirstName} {person.LastName}' already exists");

            contacts.Add(person);
            Console.WriteLine($"Contact '{person.FirstName} {person.LastName}' added successfully");
        }

        public void DeleteContact(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new InvalidInputException("First name and last name cannot be empty");

            Person? person = FindByName(firstName, lastName);
            if (person == null)
                throw new ContactNotFoundException($"Contact '{firstName} {lastName}' not found");

            contacts.Remove(person);
            Console.WriteLine($"Contact '{firstName} {lastName}' deleted successfully");
        }

        public void EditContact(string firstName, string lastName, Person updatedPerson)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new InvalidInputException("First name and last name cannot be empty");

            Person? person = FindByName(firstName, lastName);
            if (person == null)
                throw new ContactNotFoundException($"Contact '{firstName} {lastName}' not found");

            int index = contacts.IndexOf(person);
            contacts[index] = updatedPerson;
            Console.WriteLine($"Contact '{firstName} {lastName}' updated successfully");
        }

        public List<Person> GetAllContacts()
        {
            return new List<Person>(contacts);
        }

        public Person? FindByName(string firstName, string lastName)
        {
            return contacts.FirstOrDefault(p =>
                p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Person> FindByCityOrState(string cityOrState)
        {
            List<Person> result = new List<Person>();
            foreach (var person in contacts)
            {
                if (person.City.Equals(cityOrState, StringComparison.OrdinalIgnoreCase) ||
                    person.State.Equals(cityOrState, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(person);
                }
            }
            return result;
        }

        public int CountByCityOrState(string cityOrState)
        {
            int count = 0;
            foreach (var person in contacts)
            {
                if (person.City.Equals(cityOrState, StringComparison.OrdinalIgnoreCase) ||
                    person.State.Equals(cityOrState, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            return count;
        }

        public bool IsDuplicate(Person person)
        {
            return contacts.Contains(person);
        }

        public void SortByName()
        {
            SortingUtility.SortByName(contacts);
            Console.WriteLine("Contacts sorted by name");
        }

        public void SortByCityStateZip()
        {
            SortingUtility.SortByCityStateZip(contacts);
            Console.WriteLine("Contacts sorted by city, state, and zip");
        }

        public void DisplayAllContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found");
                return;
            }

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }
        }
    }
}
