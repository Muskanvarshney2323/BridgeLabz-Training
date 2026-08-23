using FundooNotesApp.ModelLayer.Entities;
using FundooNotesApp.ModelLayer.Models;
using FundooNotesApp.RepositoryLayer.Context;
using FundooNotesApp.RepositoryLayer.Interface;

namespace FundooNotesApp.RepositoryLayer.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(user => user.Email == email);
        }

        public UserModel AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public User? GetUserById(int userId)
        {
            return _context.Users
                .FirstOrDefault(user => user.UserId == userId);
        }

        public User? GetUserByResetToken(string token)
        {
            return _context.Users
                .FirstOrDefault(user => user.ResetPasswordToken == token);
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }
    }
}