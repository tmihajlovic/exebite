using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface ICustomerQueryRepository : IDatabaseQueryRepository<Customer, CustomerQueryModel>
    {
    }
}
