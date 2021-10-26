using System.Threading.Tasks;

namespace GroupManager.Services
{
    public interface IGroupManagerApiClient
    {
        Task<string> GetCodeName();
    }
}