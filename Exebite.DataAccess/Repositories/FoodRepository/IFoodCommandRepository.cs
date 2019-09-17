using System.Collections.Generic;
using Either;
using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public interface IFoodCommandRepository : IDatabaseCommandRepository<int, FoodInsertModel, FoodUpdateModel>
    {
        Either<Error, bool> DeactivateFoods(IList<int> foodIds);

        /// <summary>
        /// Updates existing Food record, depending on its Name and RestaurantId properties.
        /// </summary>
        /// <param name="food">Food update model.</param>
        /// <returns>True/False depending on whether update succeeded.</returns>
        Either<Error, bool> UpdateByNameAndRestaurantId(FoodUpdateModel food);
    }
}
