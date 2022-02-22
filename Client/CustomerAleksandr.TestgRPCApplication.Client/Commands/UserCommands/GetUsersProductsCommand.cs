using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Client.Services.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    internal class GetUsersProductsCommand : ICommand
    {
        private IReaderService _readerCommand;
        private UserManagement.UserManagementClient _userClient;
        private ILogger _log;

        public GetUsersProductsCommand(IReaderService readerCommand, UserManagement.UserManagementClient productClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _userClient = productClient;
            _log = log;
        }
        public async Task Execute()
        {
            var userId = _readerCommand.ReadInt();

            var reply = await _userClient.GetUserProductsAsync(new UserId { Id = userId.Result });

            if (reply != null && reply.ProductList.Any())
            {
                foreach (var replyProduct in reply.ProductList)
                {
                    Console.WriteLine($"{replyProduct.Id}, {replyProduct.Title}, Price: {replyProduct.Price}, Count: {replyProduct.Count}");
                }

                _log.Information($"Get Users Products userId = {userId} successfully");
            }
            else
            {
                Console.WriteLine("Enter a valid value");

                _log.Information($"Get Users Products userId = {userId} unsuccessfully");
            }
        }
    }
}
