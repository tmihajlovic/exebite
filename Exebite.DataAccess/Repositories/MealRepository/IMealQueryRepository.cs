using System.Collections.Generic;
using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IMealQueryRepository : IDatabaseQueryRepository<Meal, MealQueryModel>
    {
        Either<Error, long> FindByNameAndRestaurantId(MealQueryModel queryModel);

        Either<Error, PagingResult<Meal>> GetCondimentsForMeal(MealQueryModel queryModel);
    }
}
