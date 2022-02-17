using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Services
{
    public class UserService : UserManagement.UserManagementBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public override Task<UserResponse> AddUser(User newUser, ServerCallContext context)
        {
            try
            {
                var newId = _userService.AddUser(new Logic.Entities.User(){ Name = newUser.Name, Surname = newUser.Surname });

                return Task.FromResult(new UserResponse { Id = newId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddUser failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<Users> GetUsers(Empty a, ServerCallContext context)
        {
            try
            {
                List<Logic.Entities.User> users = _userService.GetAll();

                var returnUsers = new Users();
                foreach (var user in users)
                {
                    returnUsers.UsersList.Add(new User() { Id = user.Id, Name = user.Name, Surname = user.Surname });
                }
                return Task.FromResult(returnUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUsers failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<User> GetUserById(UserId userId, ServerCallContext context)
        {
            try
            {
                var user = _userService.GetUserById(userId.Id);

                return Task.FromResult(new User() { Id = user.Id, Name = user.Name, Surname = user.Surname });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserById failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<UsersProduct> GetUserProducts(UserId userId, ServerCallContext context)
        {
            try
            {
                var user = _userService.GetUserById(userId.Id);

                var products = new UsersProduct();

                foreach (var product in user.Products)
                {
                    products.ProductList.Add(new Product() { Id = product.Id, Count = product.Count, Price = product.Price, Title = product.Title });
                }
                return Task.FromResult(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserProducts failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
