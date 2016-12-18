using Microsoft.EntityFrameworkCore;
using WhereWereWe.Repositories.Entities;

namespace WhereWereWe.Repositories
{
    internal class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
    }
}
