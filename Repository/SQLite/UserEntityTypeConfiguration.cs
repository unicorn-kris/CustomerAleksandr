using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;

namespace Repository.SQLite
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
