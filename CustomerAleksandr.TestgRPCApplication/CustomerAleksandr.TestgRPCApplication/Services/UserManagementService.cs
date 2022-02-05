using Dependencies;
using Exceptions;
using Grpc.Core;
using LogicInterface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication
{
    public class UserManagementService : UserManagement.UserManagementBase
    {
        private ILogic _logic =  MyDependencyResolver.ILogic;

        private readonly ILogger<UserManagementService> _logger;
        public UserManagementService(ILogger<UserManagementService> logger)
        {
            _logger = logger;
        }

        public override Task<Response> AddUser(User newUser, ServerCallContext context)
        {
            try
            {
                Entity.User addUser = new Entity.User()
                {
                    Name = newUser.Name,
                    Surname = newUser.Surname
                };

                int newId = _logic.AddUser(addUser);

                return Task.FromResult(new Response { TypeOfResponse = ResponseType.ResponseOk, ResponseMessage = "Addition was successful" });
            }
            catch
            {
                return Task.FromResult(new Response { TypeOfResponse = ResponseType.ResponseError, ResponseMessage = "Addition was not successful" });
            }
        }

        public override Task<Users> GetUsers(GetUsersParameter emptyUserParameter, ServerCallContext context)
        {
            try
            {
                List<Entity.User> users = _logic.GetUsers();

                Users returnUsers = new Users();
                foreach (var user in users)
                {
                    User newUser = new User() { Id = user.Id, Name = user.Name, Surname = user.Surname };
                    returnUsers.UsersList.Add(newUser);
                }
                return Task.FromResult(returnUsers);
            }
            catch
            {
                throw new UserManagementException();
            }
        }

        public override Task<User> GetUserById(UserId userId, ServerCallContext context)
        {
            try
            {
                Entity.User user = _logic.GetUserById(userId.Id);

                return Task.FromResult(new User() { Id = user.Id, Name = user.Name, Surname = user.Surname });
            }
            catch
            {
                throw new UserManagementException();
            }
        }
    }
}
