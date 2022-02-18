using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using CustomerAleksandr.TestgRPCApplication.Services;
using Grpc.Core;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.UserCommands
{
    internal class GetUserByIdCommand : ICommand
    {
        private IReaderService _readerCommand;
        private UserManagement.UserManagementClient _userClient;
        private ILogger _log;

        public GetUserByIdCommand(IReaderService readerCommand, UserManagement.UserManagementClient productClient, ILogger log)
        {
            _readerCommand = readerCommand;
            _userClient = productClient;
            _log = log;
        }
        public async Task Execute()
        {
            Console.WriteLine("Enter id");
            var userId = _readerCommand.ReadInt();

            try
            {
                var reply = await _userClient.GetUserByIdAsync(new UserId { Id = userId.Result });

                if (reply != null)
                {
                    Console.WriteLine($"{reply.Id}, {reply.Name}, {reply.Surname}");

                    _log.Information($"GetUserByIdCommand userId = {reply.Id} successfully");
                }
                else
                {
                    Console.WriteLine("Enter a valid value");

                    _log.Error($"GetUserByIdCommand unsuccessfully");
                }
            }
            catch (RpcException ex)
            {
                _log.Error($"GetUserByIdCommand unsuccessfully StatusCode: {ex.StatusCode} Message: {ex.Message}");
            }
        }
    }
}
