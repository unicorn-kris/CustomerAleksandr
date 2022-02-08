using Grpc.Core;
using Logic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication
{
    public class UserManagementService : UserManagement.UserManagementBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(ILogger<UserManagementService> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public override Task<Response> AddUser(User newUser, ServerCallContext context)
        {
            try
            {

                var addUser = new Logic.Entities.User()
                {
                    Name = newUser.Name,
                    Surname = newUser.Surname
                };

                var newId = _userService.AddUser(addUser);

                return Task.FromResult(new Response { Id = newId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddUser failed");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override Task<Users> GetUsers(GetUsersParameter emptyUserParameter, ServerCallContext context)
        {
            try
            {
                List<Logic.Entities.User> users = _userService.GetUsers();

                var returnUsers = new Users();
                foreach (var user in users)
                {
                    var newUser = new User() { Id = user.Id, Name = user.Name, Surname = user.Surname };
                    returnUsers.UsersList.Add(newUser);
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
    }
}
