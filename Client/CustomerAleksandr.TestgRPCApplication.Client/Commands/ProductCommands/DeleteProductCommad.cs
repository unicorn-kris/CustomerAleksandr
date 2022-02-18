using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
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

            try
            {
                var reply = await _productClient.DeleteProductAsync(new ProductId { Id = productId.Result });

                _log.Information($"DeleteProductCommand productId = {productId} successfully");
            }
            catch (RpcException ex)
            {
                _log.Error($"DeleteProductCommand unsuccessfully StatusCode: {ex.StatusCode} Message: {ex.Message}");
            }
        }
    }
}
