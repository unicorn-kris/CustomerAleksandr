using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    internal class GetProductsCommand : ICommand
    {
        private ProductManagement.ProductManagementClient _productClient;
        private ILogger _log;

        public GetProductsCommand(ProductManagement.ProductManagementClient productClient, ILogger log)
        {
            _productClient = productClient;
            _log = log;
        }

        public async Task Execute()
        {
            var reply = await _productClient.GetProductsAsync(new Empty());

            if (reply.ProductsList.Any())
            {
                foreach (var replyProduct in reply.ProductsList)
                {
                    Console.WriteLine($"{replyProduct.Id}, {replyProduct.Title}, Price: {replyProduct.Price}, Count: {replyProduct.Count}");
                }
            }

            _log.Information($"Get Products successfully");
        }
    }
}
