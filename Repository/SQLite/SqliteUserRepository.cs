using Repository.Entities;
using Repository.Exceptions;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.SQLite
{
    public class SqliteUserRepository : IUserRepository
    {
        public int AddUser(User user)
        {
            try
            {
                int userId = 0;

                using (ShopContext db = new ShopContext())
                {
                    db.Add(user);
                    db.SaveChanges();

                    userId = user.Id;
                }
                return userId;
            }
            catch
            {
                throw new UserRepositoryException();
            }

        }

        public User GetUserById(int id)
        {
            try
            {
                var newUser = new User();
                using (ShopContext db = new ShopContext())
                {
                    newUser = db.Users
                                .FirstOrDefault(c => c.Id == id);
                }
                return newUser;
            }
            catch
            {
                throw new UserRepositoryException();
            }

        }

        public List<User> GetAll()
        {
            try
            {
                using (ShopContext db = new ShopContext())
                {
                   return db.Users.ToList();
                }
            }
            catch
            {
                throw new UserRepositoryException();
            }
        }
    }
}
