using Infrastructure.Configurations;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataBaseDbContext : DbContext
    {
        public DataBaseDbContext(DbContextOptions<DataBaseDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BookEntity> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new UserConfiguration());
           modelBuilder.ApplyConfiguration(new BookConfiguration());
        }

    }
}
