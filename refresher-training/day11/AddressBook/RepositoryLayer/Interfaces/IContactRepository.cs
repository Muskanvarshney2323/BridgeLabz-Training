using ModelLayer.Entities;

namespace RepositoryLayer.Interface
{
    public interface IContactRepository
    {
        Contact Add(Contact contact);
        List<Contact> GetAll();
        Contact GetById(int id);
        Contact Update(int id, Contact contact);
        bool Delete(int id);
    }
}