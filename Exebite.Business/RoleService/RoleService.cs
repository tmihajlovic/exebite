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

        public Task<Either<Error, string>> GetRoleForGoogleUserAsync(string id) =>
            Task.FromResult(_queryRepository.GetRole(id));
    }
}
