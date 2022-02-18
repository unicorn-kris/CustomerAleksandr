using Repository.Entities;
using Repository.Exceptions;
using Repository.Interfaces;
using System;
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
            catch (Exception ex)
            {
                throw new UserRepositoryException("AddUser failed" + ex.Message);
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
            catch (Exception ex)
            {
                throw new UserRepositoryException("GetUserById failed" + ex.Message);
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
            catch (Exception ex)
            {
                throw new UserRepositoryException("GetAll failed" + ex.Message);
            }
        }
    }
}
