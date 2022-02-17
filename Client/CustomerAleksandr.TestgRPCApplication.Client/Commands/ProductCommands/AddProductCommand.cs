using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    class AddProductCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;

        public AddProductCommand(IReaderCommand readerCommand, ProductManagement.ProductManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
        }
        public async Task Execute()
        {
            var newProduct = new Product();

            Console.WriteLine("Enter title: ");
            newProduct.Title = await _readerCommand.ReadString();

            Console.WriteLine("Enter price: ");
            newProduct.Price = await _readerCommand.ReadInt();

            Console.WriteLine("Enter count: ");
            newProduct.Count = await _readerCommand.ReadInt();

            try
            {
                var reply = await _productClient.AddProductAsync(newProduct);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"StatusCode: {ex.StatusCode} Message: {ex.Message}");
            }

            return;
        }
    }
}
