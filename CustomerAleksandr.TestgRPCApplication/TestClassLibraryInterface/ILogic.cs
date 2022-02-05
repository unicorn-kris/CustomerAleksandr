using System.Collections.Generic;
using Entity;

namespace LogicInterface
{
    public interface ILogic
    {
        int AddUser(User user);
        List<User> GetUsers();
        User GetUserById(int id);
    }
}
