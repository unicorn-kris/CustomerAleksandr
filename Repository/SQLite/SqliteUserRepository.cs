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
                using (ShopContext db = new ShopContext())
                {
                    db.Add(user);
                    db.SaveChanges();

                    return user.Id;
                }
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("AddUser failed ", ex);
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                using (ShopContext db = new ShopContext())
                {
                    return db.Users
                                .FirstOrDefault(c => c.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("GetUserById failed ", ex);
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
                throw new UserRepositoryException("GetAll failed ", ex);
            }
        }
    }
}
