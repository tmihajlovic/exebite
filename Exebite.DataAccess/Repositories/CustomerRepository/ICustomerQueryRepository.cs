using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface ICustomerQueryRepository : IDatabaseQueryRepository<Customer, CustomerQueryModel>
    {
        Either<Error, string> GetRole(string googleId);
    }
}
