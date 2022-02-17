using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    class BuyProductCommand : ICommand
    {
        private IReaderCommand _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;

        public BuyProductCommand(IReaderCommand readerCommand, ProductManagement.ProductManagementClient productManagementClient)
        {
            _readerCommand = readerCommand;
            _productClient = productManagementClient;
        }
        public async Task Execute()
        {

            Console.WriteLine("Enter product id");

            var productId = _readerCommand.ReadInt();

            Console.WriteLine("Enter user id");

            var userId = _readerCommand.ReadInt();

            var reply = await _productClient.BuyProductAsync(new BuyProductRequest { ProductId = productId.Result, UserId = userId.Result });

            if (reply != new Empty())
            {
                Console.WriteLine("Enter a valid value");
            }

        }
    }
}
