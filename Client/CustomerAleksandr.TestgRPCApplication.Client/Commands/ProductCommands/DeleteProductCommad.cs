using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Client.Services.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    internal class DeleteProductCommad : ICommand
    {
        private IReaderService _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;
        private ILogger _log;

        public DeleteProductCommad(IReaderService readerCommand, ProductManagement.ProductManagementClient productClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");
            var productId = _readerCommand.ReadInt();

            await _productClient.DeleteProductAsync(new ProductId { Id = productId.Result });

            _log.Information($"Delete Product productId = {productId} successfully");
        }
    }
}
