using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.SQLite
{
    internal class ShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public ShopContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Shop.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());

            new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
        }
    }
}
