using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IMealQueryRepository : IDatabaseQueryRepository<Meal, MealQueryModel>
    {
        Either<Error, long> FindByNameAndRestaurantId(MealQueryModel queryModel);
    }
}
