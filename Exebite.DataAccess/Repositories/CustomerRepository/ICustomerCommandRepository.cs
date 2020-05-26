using Either;
using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public interface ICustomerCommandRepository : IDatabaseCommandRepository<long, CustomerInsertModel, CustomerUpdateModel>
    {
        Either<Error, bool> UpdateByGoogleId(CustomerUpdateModel customer);
    }
}