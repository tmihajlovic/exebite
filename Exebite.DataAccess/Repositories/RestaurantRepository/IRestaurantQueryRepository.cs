using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IRestaurantQueryRepository : IDatabaseQueryRepository<Restaurant, RestaurantQueryModel>
    {
    }
}
