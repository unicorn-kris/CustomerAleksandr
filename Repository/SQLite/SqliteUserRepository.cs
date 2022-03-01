using Repository.Entities;
using Repository.Exceptions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RepositoryTests")]

namespace Repository.SQLite
{
    public class SQLiteUserRepository : IUserRepository
    {
        private IContext _context;

        public SQLiteUserRepository(IContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            try
            {
                using (_context)
                {
                    _context.Add(user);
                    _context.SaveChanges();

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
                using (_context)
                {
                    return _context.Users
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
                using (_context)
                {
                   return _context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new UserRepositoryException("GetAll failed ", ex);
            }
        }
    }
}
