using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    class GetUsersProductsCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private UserManagement.UserManagementClient _userClient;

        public GetUsersProductsCommand(IReaderCommand readerCommand, UserManagement.UserManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _userClient = productClient;
        }
        public async Task Execute()
        {
            var userId = _readerCommand.ReadInt();
            var reply = await _userClient.GetUserProductsAsync(new UserId { Id = userId.Result });

            if (reply != null)
            {
                foreach (var replyProduct in reply.ProductList)
                {
                    Console.WriteLine($"{replyProduct.Id}, {replyProduct.Title}, Price: {replyProduct.Price}, Count: {replyProduct.Count}");
                }
            }
            else
            {
                Console.WriteLine("Enter a valid value");
            }
        }
    }
}
