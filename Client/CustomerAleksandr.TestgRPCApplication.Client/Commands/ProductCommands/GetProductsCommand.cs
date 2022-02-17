using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    class GetProductsCommand : ICommand
    {
        private ProductManagement.ProductManagementClient _productClient;

        public GetProductsCommand(ProductManagement.ProductManagementClient productClient)
        {
            _productClient = productClient;
        }

        public async Task Execute()
        {
            var reply = await _productClient.GetProductsAsync(new Empty());

            if (reply != null)
                foreach (var replyProduct in reply.ProductsList)
                {
                    Console.WriteLine($"{replyProduct.Id}, {replyProduct.Title}, Price: {replyProduct.Price}, Count: {replyProduct.Count}");
                }
        }
    }
}
