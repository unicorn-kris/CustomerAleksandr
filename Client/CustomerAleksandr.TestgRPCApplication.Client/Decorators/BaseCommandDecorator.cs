using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using Serilog;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Decorators
{
    abstract class BaseCommandDecorator : ICommand
    {
        protected ICommand _command;
        protected ILogger _logger;

        public BaseCommandDecorator(ILogger logger, ICommand command)
        {
            _command = command;
            _logger = logger;
        }

        public abstract Task Execute();
    }
}
