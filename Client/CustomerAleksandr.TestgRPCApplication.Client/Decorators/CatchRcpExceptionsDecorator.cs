using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using Grpc.Core;
using Serilog;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Decorators
{
    internal class CatchRcpExceptionsDecorator : BaseCommandDecorator
    {
        public CatchRcpExceptionsDecorator(ILogger logger, ICommand command) : base(logger, command)
        {
        }
        public override async Task Execute()
        {
            try
            {
               await _command.Execute();
            }
            catch (RpcException ex)
            {
                _logger.Error(ex, $" {_command.GetType().Name} Failed");
            }
        }
    }
}
