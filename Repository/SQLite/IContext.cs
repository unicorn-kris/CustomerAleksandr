using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.SQLite
{
    public abstract class IContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Product> Products { get; set; }
    }
}
