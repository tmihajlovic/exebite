using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IOrderQueryRepository : IDatabaseQueryRepository<Order, OrderQueryModel>
    {
    }
}
