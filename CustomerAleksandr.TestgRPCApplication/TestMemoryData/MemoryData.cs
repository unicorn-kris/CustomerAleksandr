using DataInterface;
using Entity;
using Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace TestMemoryData
{
    public class MemoryData : IData
    {
        private List<User> _listOfUsers = new List<User>();

        public int AddUser(User user)
        {
            try
            {
                if (!_listOfUsers.Any())
                {
                    user.Id = 1;
                }
                else
                {
                    user.Id = _listOfUsers.Last().Id + 1;
                }

                _listOfUsers.Add(user);
                return user.Id;
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
                return _listOfUsers.FirstOrDefault(user => user.Id == id);
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
                return _listOfUsers;
            }
            catch
            {
                throw new UserManagementException();
            }
        }
    }
}
