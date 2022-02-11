using System.Collections.Generic;
using Logic.Entities;

namespace Logic.Interfaces
{
    public interface IUserService
    {
        int AddUser(User user);

        List<User> GetAll();

        User GetUserById(int id);
    }
}
