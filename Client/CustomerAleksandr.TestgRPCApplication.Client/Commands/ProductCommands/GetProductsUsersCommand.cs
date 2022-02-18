using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ProductCommands
{
    internal class GetProductsUsersCommand : ICommand
    {
        private IReaderService _readerCommand;
        private ProductManagement.ProductManagementClient _productClient;
        private ILogger _log;

        public GetProductsUsersCommand(IReaderService readerCommand, ProductManagement.ProductManagementClient productClient, ILogger log)
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
                var reply = await _productClient.GetProductUsersAsync(new ProductId { Id = productId.Result });

                if (reply != null && reply.UserList.Any())
                {
                    foreach (var user in reply.UserList)
                    {
                        Console.WriteLine($"{user.Id}, {user.Name}, {user.Surname}");
                    }

                    _log.Information($"Get Products Users productId = {productId} successfully");
                }
                else
                {
                    _log.Error($"Get Products Users unsuccessfully");

                    Console.WriteLine("Enter a valid value");
                }
            }
            catch (RpcException ex)
            {
                _log.Error(ex, "Get Products Users Failed");
            }
        }
    }
}
