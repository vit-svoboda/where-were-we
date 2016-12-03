using Microsoft.EntityFrameworkCore;
using WhereWereWe.Repositories.Entities;

namespace WhereWereWe.Repositories
{
    public class SeriesContext : DbContext
    {
        public DbSet<Series> Series { get; set; }

        public SeriesContext(DbContextOptions<SeriesContext> options)
            : base(options)
        {
        }
    }
}
