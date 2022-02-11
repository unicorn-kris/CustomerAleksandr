using Repository.Entities;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        int AddUser(User user);

        List<User> GetAll();

        User GetUserById(int id);
    }
}
