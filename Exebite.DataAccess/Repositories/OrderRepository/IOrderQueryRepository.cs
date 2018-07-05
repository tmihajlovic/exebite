using Either;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IOrderQueryRepository : IDatabaseQueryRepository<Order, OrderQueryModel>
    {
        Either<Error, PagingResult<Order>> GetAllOrdersForRestaurant(int restaruntId, int page, int size);
    }
}
