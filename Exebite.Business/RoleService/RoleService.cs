using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;

namespace Exebite.Business
{
    public class RoleService : IRoleService
    {
        private ICustomerQueryRepository _queryRepository;

        public RoleService(ICustomerQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public Task<Either<Error, string>> GetRoleForGoogleUserAsync(IEnumerable<Claim> claims)
        {
            string userId = claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
            return Task.FromResult(_queryRepository.GetRole(userId));
        }
    }
}
