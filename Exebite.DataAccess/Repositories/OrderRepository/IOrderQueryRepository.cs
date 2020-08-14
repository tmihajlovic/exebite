using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IOrderQueryRepository : IDatabaseQueryRepository<Order, OrderQueryModel>
    {
        Either<Error, PagingResult<Order>> GetAllOrdersForRestaurant(long restaruntId, int page, int size);
    }
}
