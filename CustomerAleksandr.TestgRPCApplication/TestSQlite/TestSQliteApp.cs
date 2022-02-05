using DataInterface;
using Entity;
using System;
using System.Collections.Generic;

namespace TestSQlite
{
    public class TestSQliteApp : IData
    {
        public int AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
