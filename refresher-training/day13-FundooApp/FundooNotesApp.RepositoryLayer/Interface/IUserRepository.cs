using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.ModelLayer.Models;

namespace FundooNotesApp.RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        UserModel AddUser(User user);

        User? GetUserById(int userId);

        User? GetUserByResetToken(string token);

        bool UpdateUser(User user);
    }
}