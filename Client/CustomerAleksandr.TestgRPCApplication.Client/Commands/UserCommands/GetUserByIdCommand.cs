using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    class GetUserByIdCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private UserManagement.UserManagementClient _userClient;

        public GetUserByIdCommand(IReaderCommand readerCommand, UserManagement.UserManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _userClient = productClient;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");

            var userId = _readerCommand.ReadInt();
            var reply = await _userClient.GetUserByIdAsync(new UserId { Id = userId.Result });

            if (reply != null)
            {
                Console.WriteLine($"{reply.Id}, {reply.Name}, {reply.Surname}");
            }
            else
            {
                Console.WriteLine("Enter a valid value");
            }
        }
    }
}
