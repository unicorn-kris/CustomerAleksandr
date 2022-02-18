using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    internal class BuyProductCommand : ICommand
    {
        private IReaderService _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;
        private ILogger _log;

        public BuyProductCommand(IReaderService readerCommand, ProductManagement.ProductManagementClient productManagementClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _productClient = productManagementClient;
            _log = log;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter product id");
            var productId = _readerCommand.ReadInt();

            Console.WriteLine("Enter user id");
            var userId = _readerCommand.ReadInt();

            try
            {
                var reply = await _productClient.BuyProductAsync(new BuyProductRequest { ProductId = productId.Result, UserId = userId.Result });

                _log.Information($"BuyProductCommand productId = {productId}, userId = {userId} successfully");
            }
            catch (RpcException ex)
            {
                _log.Error($"BuyProductCommand unsuccessfully StatucsCode: {ex.StatusCode} Message: {ex.Message}");
            }
        }
    }
}
