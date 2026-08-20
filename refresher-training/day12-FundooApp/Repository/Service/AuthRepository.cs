using Models.Entity;
using Repository.Context;
using Repository.Interface;

namespace Repository.Service
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FundooDbContext _context;

        public AuthRepository(FundooDbContext context)
        {
            _context = context;
        }

        public User Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User? Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(
                x => x.Email == email &&
                     x.Password == password
            );
        }
    }
}