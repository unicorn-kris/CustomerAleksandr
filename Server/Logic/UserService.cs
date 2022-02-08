using Logic.Entities;
using Logic.Exceptions;
using Repository;
using System.Collections.Generic;

namespace Logic
{
    public class UserService : IUserService
    {
        private IRepository _repository;

        public UserService(IRepository data)
        {
            _repository = data;
        }

        public int AddUser(User user)
        {
            try
            {
                var newUser = new Repository.Entities.User();
                newUser.Name = user.Name;
                newUser.Surname = user.Surname;

                return _repository.AddUser(newUser);
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
                var newUser = _repository.GetUserById(id);

                return (new User { Id = id, Name = newUser.Name, Surname = newUser.Surname });
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
                var listUsers = _repository.GetUsers();
                List<User> resultListOfUsers = new List<User>();

                foreach(var user in listUsers)
                {
                    resultListOfUsers.Add(new User { Name = user.Name, Surname = user.Surname, Id = user.Id });
                }

                return resultListOfUsers;
            }
            catch
            {
                throw new UserManagementException();
            }
        }

    }
}
