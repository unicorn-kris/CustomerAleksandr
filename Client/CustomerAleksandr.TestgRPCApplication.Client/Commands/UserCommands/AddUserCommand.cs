using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    internal class AddUserCommand : ICommand
    {
        private IReaderService _readerCommand;
        private UserManagement.UserManagementClient _userClient;
        private ILogger _log;

        public AddUserCommand(IReaderService readerCommand, UserManagement.UserManagementClient productClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _userClient = productClient;
            _log = log;
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

                _log.Information($"Add User userId = {reply.Id} successfully");
            }
            catch (RpcException ex)
            {
                _log.Error(ex, "Add User Failed");
            }
        }
    }
}
