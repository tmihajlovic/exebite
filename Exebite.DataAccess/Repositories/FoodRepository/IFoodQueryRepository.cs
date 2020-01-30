using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IFoodQueryRepository : IDatabaseQueryRepository<Food, FoodQueryModel>
    {
        /// <summary>
        /// Check whether Food object with specified Name and RestaurantId exists.
        /// </summary>
        /// <param name="queryModel">Food query model.</param>
        /// <returns>Objects Id if exists, or 0 if it doesn't.</returns>
        Either<Error, int> FindByNameAndRestaurantId(FoodQueryModel queryModel);
    }
}
