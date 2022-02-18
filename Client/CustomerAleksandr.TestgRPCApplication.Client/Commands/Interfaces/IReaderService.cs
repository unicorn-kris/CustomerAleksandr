using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Commands.Interfaces
{
    public interface IReaderService
    {
        Task<string> ReadString();

        Task<int> ReadInt();
    }
}
