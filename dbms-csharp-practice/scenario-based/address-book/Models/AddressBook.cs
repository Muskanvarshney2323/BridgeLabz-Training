namespace AddressBookSystem.Models
{
    public class AddressBook
    {
        public string Name { get; set; }
        private List<Person> contacts;

        public AddressBook(string name)
        {
            Name = name;
            contacts = new List<Person>();
        }

        public List<Person> GetContacts()
        {
            return new List<Person>(contacts);
        }

        public void AddContact(Person person)
        {
            contacts.Add(person);
        }

        public void RemoveContact(Person person)
        {
            contacts.Remove(person);
        }

        public int Count()
        {
            return contacts.Count;
        }

        public bool ContainsContact(Person person)
        {
            return contacts.Contains(person);
        }

        public void Clear()
        {
            contacts.Clear();
        }
    }
}
