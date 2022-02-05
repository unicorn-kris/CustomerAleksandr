using Entity;
using System.Collections.Generic;

namespace DataInterface
{
    public interface IData
    {
        int AddUser(User user);
        List<User> GetUsers();
        User GetUserById(int id);
    }
}
