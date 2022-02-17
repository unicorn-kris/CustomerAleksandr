using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ReaderCommands
{
    class ConsoleReaderCommand : IReaderCommand
    {
        public Task<string> ReadString()
        {
            return Task.FromResult(Console.ReadLine());
        }

        Task<int> IReaderCommand.ReadInt()
        {
            var result = -1;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Enter a valid value");
            }
            return Task.FromResult(result);
        }
    }
}
