using System.Text;

interface IAddressBook
{
    void AddContactPerson(ContactPerson person);
    void EditContactPerson(string firstname);
    public void DeleteContact(string firstName);
}
