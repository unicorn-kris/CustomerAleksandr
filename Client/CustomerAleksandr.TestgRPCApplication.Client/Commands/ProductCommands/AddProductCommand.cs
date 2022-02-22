using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Client.Services.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    internal class AddProductCommand : ICommand
    {
        private IReaderService _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;
        private ILogger _log;

        public AddProductCommand(IReaderService readerCommand, ProductManagement.ProductManagementClient productClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
            _log = log;
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

            var reply = await _productClient.AddProductAsync(newProduct);

            _log.Information($"Add Product productId = {reply.Id} successfully");
        }
    }
}
