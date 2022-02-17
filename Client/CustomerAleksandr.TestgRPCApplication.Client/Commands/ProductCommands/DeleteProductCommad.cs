using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    class DeleteProductCommad : ICommand
    {
        private IReaderCommand _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;

        public DeleteProductCommad(IReaderCommand readerCommand, ProductManagement.ProductManagementClient productClient)
        {
            _readerCommand = readerCommand;
            _productClient = productClient;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");

            var productId = _readerCommand.ReadInt();

            var reply = await _productClient.DeleteProductAsync(new ProductId { Id = productId.Result });

            if (reply != new Empty())
            {
                Console.WriteLine("Enter a valid value");
            }

        }
    }
}
