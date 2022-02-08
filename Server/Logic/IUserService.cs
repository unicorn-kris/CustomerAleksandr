using System.Collections.Generic;
using Logic.Entities;

namespace Logic
{
    public interface IUserService
    {
        int AddUser(User user);

        List<User> GetUsers();

        User GetUserById(int id);
    }
}
