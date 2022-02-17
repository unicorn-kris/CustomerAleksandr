using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    class AddUserCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private UserManagement.UserManagementClient _userClient;

        public AddUserCommand(IReaderCommand readerCommand, UserManagement.UserManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _userClient = productClient;
        }
        public async Task Execute()
        {
            var newUser = new User();

            Console.WriteLine("Enter name: ");
            newUser.Name = await _readerCommand.ReadString();

            Console.WriteLine("Enter surname: ");
            newUser.Surname = await _readerCommand.ReadString();

            try
            {
                var reply = await _userClient.AddUserAsync(newUser);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode} Message: {ex.Message}");
            }
        }
    }
}
