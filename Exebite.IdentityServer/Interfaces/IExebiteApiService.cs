using Exebite.IdentityServer.Model;
using System.Threading.Tasks;

namespace Exebite.IdentityServer.Interfaces
{
    public interface IExebiteApiService
    {
        Task<UserInfo> GetUserInfo(string id);
    }
}
