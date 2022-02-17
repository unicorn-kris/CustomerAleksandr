using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces
{
    public interface IReaderCommand
    {
        Task<string> ReadString();

        Task<int> ReadInt();
    }
}
