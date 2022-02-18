using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    internal class GetProductByIdCommand : ICommand
    {
        private IReaderService _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;
        private ILogger _log;

        public GetProductByIdCommand(IReaderService readerCommand, ProductManagement.ProductManagementClient productClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
            _log = log;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");
            var productId = _readerCommand.ReadInt();

            try
            {
                var reply = await _productClient.GetProductByIdAsync(new ProductId { Id = productId.Result });

                if (reply != null)
                {
                    Console.WriteLine($"{reply.Id}, {reply.Title}, Price: {reply.Price}, Count: {reply.Count}");

                    _log.Information($"AddProductCommand productId = {reply.Id} successfully");
                }
                else
                {
                    Console.WriteLine("Enter a valid value");

                    _log.Error($"GetProductByIdCommand unsuccessfully");
                }
            }
            catch (RpcException ex)
            {
                _log.Error($"GetProductByIdCommand unsuccessfully StatusCode: {ex.StatusCode} Message: {ex.Message}");
            }
        }
    }
}
