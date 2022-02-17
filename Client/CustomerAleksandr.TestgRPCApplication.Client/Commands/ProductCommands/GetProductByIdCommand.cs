using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    class GetProductByIdCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;

        public GetProductByIdCommand(IReaderCommand readerCommand, ProductManagement.ProductManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");

            var productId = _readerCommand.ReadInt();

            var reply = await _productClient.GetProductByIdAsync(new ProductId { Id = productId.Result });

            if (reply != null)
            {
                Console.WriteLine($"{reply.Id}, {reply.Title}, Price: {reply.Price}, Count: {reply.Count}");
            }
            else
            {
                Console.WriteLine("Enter a valid value");
            }
        }
    }
}
