using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Client.Services.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
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

            var reply = await _productClient.GetProductByIdAsync(new ProductId { Id = productId.Result });

            if (reply != null)
            {
                Console.WriteLine($"{reply.Id}, {reply.Title}, Price: {reply.Price}, Count: {reply.Count}");

                _log.Information($"Get Product By Id productId = {reply.Id} successfully");
            }
            else
            {
                Console.WriteLine("Enter a valid value");

                _log.Error($"Get Product By Id unsuccessfully");
            }
        }
    }
}
