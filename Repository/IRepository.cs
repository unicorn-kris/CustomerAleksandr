using Repository.Entities;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository
    {
        int AddUser(User user);

        List<User> GetUsers();

        User GetUserById(int id);
    }
}
