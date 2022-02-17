using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    class GetProductsUsersCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;

        public GetProductsUsersCommand(IReaderCommand readerCommand, ProductManagement.ProductManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");

            var productId = _readerCommand.ReadInt();
            var reply = await _productClient.GetProductUsersAsync(new ProductId { Id = productId.Result });

            if (reply != null)
            {
                foreach (var user in reply.UserList)
                {
                    Console.WriteLine($"{user.Id}, {user.Name}, {user.Surname}");
                }
            }
            else
            {
                Console.WriteLine("Enter a valid value");
            }
        }
    }
}
