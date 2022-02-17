using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    class GetUsersCommand : ICommand
    {
        private UserManagement.UserManagementClient _userClient;

        public GetUsersCommand(UserManagement.UserManagementClient productClient)
        {
            _userClient = productClient;
        }

        public async Task Execute()
        {
            var reply = await _userClient.GetUsersAsync(new Empty());

            if (reply != null)
            {
                foreach (var user in reply.UsersList)
                {
                    Console.WriteLine($"{user.Id}, {user.Name}, {user.Surname}");
                }
            }
        }
    }
}
