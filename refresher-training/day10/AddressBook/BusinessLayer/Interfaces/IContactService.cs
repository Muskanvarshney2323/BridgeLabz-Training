using ModelLayer.Dtos;
using ModelLayer.Entities;

namespace BusinessLayer.Interface
{
    public interface IContactService
    {
        Contact Add(ContactDto contact);
        List<Contact> GetAll();
        Contact GetById(int id);
        Contact Update(int id, ContactDto contact);
        bool Delete(int id);
    }
}