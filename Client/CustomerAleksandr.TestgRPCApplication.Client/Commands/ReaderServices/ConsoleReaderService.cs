using CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces;
using System;
using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.ReaderServices
{
    internal class ConsoleReaderService : IReaderService
    {
        public Task<string> ReadString()
        {
            return Task.FromResult(Console.ReadLine());
        }

        public Task<int> ReadInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Enter a valid value");
            }
            return Task.FromResult(result);
        }
    }
}
