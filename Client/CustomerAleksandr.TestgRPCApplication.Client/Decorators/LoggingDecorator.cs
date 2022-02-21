using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using Serilog;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Decorators
{
    internal class LoggingDecorator : BaseCommandDecorator
    {
        public LoggingDecorator(ILogger logger, ICommand command) : base(logger, command)
        {
        }

        public override async Task Execute()
        {
            _logger.Information($"{_command.GetType().Name} started");
            await _command.Execute();
            _logger.Information($"{_command.GetType().Name} finished");
        }
    }
}
