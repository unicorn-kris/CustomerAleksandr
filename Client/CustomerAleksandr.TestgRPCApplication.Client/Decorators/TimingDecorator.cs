using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using Serilog;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Decorators
{
    internal class TimingDecorator : BaseCommandDecorator
    {
        public TimingDecorator(ILogger logger, ICommand command) : base(logger, command)
        {
        }

        public override async Task Execute()
        {
            var timer = Stopwatch.StartNew();
            await _command.Execute();
            _logger.Information($"Elapsed {timer.Elapsed}");
        }
    }
}
