using System.Threading.Tasks;

namespace CustomerAleksandr.TestgRPCApplication.Client.Services.Interfaces
{
    public interface IReaderService
    {
        Task<string> ReadString();

        Task<int> ReadInt();
    }
}
