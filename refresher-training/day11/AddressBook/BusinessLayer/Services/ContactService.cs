using ModelLayer.Dtos;
using ModelLayer.Entities;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository repository;

        public ContactService(IContactRepository repository)
        {
            this.repository = repository;
        }

        public Contact Add(ContactDto contactDto)
        {
            Contact contact = new Contact
            {
                Name = contactDto.Name,
                Phone = contactDto.Phone,
                Email = contactDto.Email
            };

            return repository.Add(contact);
        }

        public List<Contact> GetAll()
        {
            return repository.GetAll();
        }

        public Contact GetById(int id)
        {
            return repository.GetById(id);
        }

        public Contact Update(int id, ContactDto contactDto)
        {
            Contact contact = new Contact
            {
                Name = contactDto.Name,
                Phone = contactDto.Phone,
                Email = contactDto.Email
            };

            return repository.Update(id, contact);
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}