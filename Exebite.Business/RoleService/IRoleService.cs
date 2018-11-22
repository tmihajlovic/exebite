using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Either;
using Exebite.Common;

namespace Exebite.Business
{
    public interface IRoleService
    {
        Task<Either<Error, string>> GetRoleForGoogleUserAsync(IEnumerable<Claim> claims);
    }
}