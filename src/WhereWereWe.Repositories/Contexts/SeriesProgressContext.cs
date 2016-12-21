using Microsoft.EntityFrameworkCore;
using WhereWereWe.Repositories.Entities;

namespace WhereWereWe.Repositories.Contexts
{
    internal class SeriesProgressContext : DbContext
    {
        public DbSet<SeriesProgress> SeriesProgress { get; set; }

        public DbSet<Series> Series { get; set; }

        public DbSet<User> Users { get; set; }

        public SeriesProgressContext(DbContextOptions<SeriesProgressContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
