using System.Collections.Generic;
using Either;

namespace Exebite.DataAccess.Repositories
{
    public interface IFoodCommandRepository : IDatabaseCommandRepository<int, FoodInsertModel, FoodUpdateModel>
    {
        Either<Error, bool> DeactivatFoods(IList<int> foodIds);

    }
}
