using Microsoft.EntityFrameworkCore;
using Models.Entity;

namespace Repository.Context
{
    public class FundooDbContext : DbContext
    {
        public FundooDbContext(
            DbContextOptions<FundooDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}