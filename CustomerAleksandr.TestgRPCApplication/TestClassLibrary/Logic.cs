using DataInterface;
using Entity;
using Exceptions;
using LogicInterface;
using System.Collections.Generic;

namespace MyLogic
{
    public class Logic : ILogic
    {
        private IData _data;

        public Logic(IData data)
        {
            _data = data;
        }

        public int AddUser(User user)
        {
            try
            {
                return _data.AddUser(user);
            }
            catch
            {
                throw new UserManagementException();
            }
        }


        public User GetUserById(int id)
        {
            try
            {
                return _data.GetUserById(id);
            }
            catch
            {
                throw new UserManagementException();
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return _data.GetUsers();
            }
            catch
            {
                throw new UserManagementException();
            }
        }

    }
}
