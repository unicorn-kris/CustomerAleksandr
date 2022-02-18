using Logic.Entities;
using Logic.Exceptions;
using Logic.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Logic.Services
{
    internal class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository data)
        {
            _userRepository = data;
        }

        public int AddUser(User user)
        {
            try
            {
                var newUser = new Repository.Entities.User();
                newUser.Name = user.Name;
                newUser.Surname = user.Surname;

                return _userRepository.AddUser(newUser);
            }
            catch (Exception ex)
            {
                throw new UserLogicException("GetUserById failed ", ex);
            }
        }

        public List<User> GetAll()
        {
            try
            {
                var result = new List<User>();

                var users = _userRepository.GetAll();

                foreach (var user in users)
                {
                    var newUser = new User
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname
                    };

                    foreach (var product in user.Products)
                    {
                        newUser.Products.Add(new Product
                        {
                            Id = product.Id,
                            Title = product.Title,
                            Count = product.Count,
                            Price = product.Price
                        });
                    }

                    result.Add(newUser);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new UserLogicException("GetUserById failed ", ex);
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                var newUser = _userRepository.GetUserById(id);

                return (new User { Id = id, Name = newUser.Name, Surname = newUser.Surname });
            }
            catch (Exception ex)
            {
                throw new UserLogicException("GetUserById failed ", ex);
            }
        }
    }
}
