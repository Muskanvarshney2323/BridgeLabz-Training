using Models.Entity;

namespace Repository.Interface
{
    public interface IAuthRepository
    {
        User Register(User user);

        User Login(string email, string password);
    }
}