using System.Collections.Generic;
using Either;
using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public interface IMealCommandRepository : IDatabaseCommandRepository<long, MealInsertModel, MealUpdateModel>
    {
        Either<Error, bool> DeactivateMeals(IList<long> ids);

        Either<Error, bool> UpdateByNameAndRestaurantId(MealUpdateModel meal);
    }
}
