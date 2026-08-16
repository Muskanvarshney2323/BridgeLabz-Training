using ModelLayer.Entities;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext db;

        public ContactRepository(AppDbContext db)
        {
            this.db = db;
        }

        public Contact Add(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();

            return contact;
        }

        public List<Contact> GetAll()
        {
            return db.Contacts.ToList();
        }

        public Contact GetById(int id)
        {
            return db.Contacts.FirstOrDefault(x => x.ContactId == id);
        }

        public Contact Update(int id, Contact contact)
        {
            var oldContact = db.Contacts.FirstOrDefault(x => x.ContactId == id);

            if (oldContact == null)
                return null;

            oldContact.Name = contact.Name;
            oldContact.Phone = contact.Phone;
            oldContact.Email = contact.Email;

            db.SaveChanges();

            return oldContact;
        }

        public bool Delete(int id)
        {
            var contact = db.Contacts.FirstOrDefault(x => x.ContactId == id);

            if (contact == null)
                return false;

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return true;
        }
    }
}